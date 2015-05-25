using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.Tcp.Session
{
    class IocpSessionFrameBuffer
    {
        private object receiveBufferLocker { get; set; }
        private DescriptorBuffer receiveBuffer { get; set; }

        public ConcurrentQueue<Descriptor> sendQueue { get; set; }

        public IocpSessionFrameBuffer()
        {
            receiveBufferLocker = new object();

            receiveBuffer = DescriptorBuffer.create(128 * 1024);

            sendQueue = new ConcurrentQueue<Descriptor>();
        }

        public void resetBuffer()
        {
            receiveBuffer.clear();

            Descriptor ignored;
            while (sendQueue.TryDequeue(out ignored)) { };
        }

        public void putSendStream(byte[] buffer, int offset, int count)
        {
            IocpSessionFrameHeader header = new IocpSessionFrameHeader();
            header.payloadLength = count;
            byte[] headerBytes = header.serialize();
            DescriptorBuffer frameBuffer = DescriptorBuffer.create(count + headerBytes.Length);
            frameBuffer.append(headerBytes, 0, headerBytes.Length);
            frameBuffer.append(buffer, offset, count);
            sendQueue.Enqueue(frameBuffer);
            //Logger.debug("IocpSessionFrameBuffer: {0} bytes put into send buffer.", count);
        }

        public Descriptor getSendFrameIfHasOne()
        {
            //Logger.debug("IocpSessionFrameBuffer: try get one frame from send buffer.");
            Descriptor sendBuffer;
            if (sendQueue.TryDequeue(out sendBuffer) == true)
            {
                Logger.debug("IocpSessionFrameBuffer: frame got from send buffer.");
                return sendBuffer;
            }
            else
            {
                //Logger.debug("IocpSessionFrameBuffer: send buffer is empty.");
                return null;
            }
        }

        public void putReceiveDataStream(Descriptor descriptor)
        {
            //Logger.debug("IocpSessionFrameBuffer: {0} bytes put into receive buffer.", descriptor.desLength);
            lock (receiveBufferLocker)
            {
                receiveBuffer.append(descriptor.des, 0, descriptor.desLength);
            }
        }

        public Descriptor getReceivedFrameIfHasOne()
        {
            Logger.debug("IocpSessionFrameBuffer: try get one frame from receive buffer.");
            lock (receiveBufferLocker)
            {
                if(receiveBuffer.desLength < IocpSessionFrameHeader.headerLength)
                {
                    //Logger.debug("IocpSessionFrameBuffer: receive buffer is empty.");
                    return null;
                }
                IocpSessionFrameHeader header = IocpSessionFrameHeader.deserialize(receiveBuffer.des);
                if(header == null)
                {
                    Logger.error("IocpSessionFrameBuffer: receive buffer is currupted.");
                    return null;
                }
                int newFrameLength = header.payloadLength;
                int newFrameLengthWithHeader = IocpSessionFrameHeader.headerLength + newFrameLength;
                if(receiveBuffer.desLength < newFrameLengthWithHeader)
                {
                    //Logger.debug("IocpSessionFrameBuffer: frame fragments! receive buffer is waiting for more data.");
                    return null;
                }
                DescriptorBuffer newFrame = DescriptorBuffer.create(
                    receiveBuffer.des,
                    IocpSessionFrameHeader.headerLength,
                    newFrameLength,
                    newFrameLength);
                receiveBuffer.truncate(
                    newFrameLengthWithHeader,
                    receiveBuffer.desLength - newFrameLengthWithHeader);
                Logger.debug("IocpSessionFrameBuffer: frame got from receive buffer.");
                return newFrame;
            }
        }
    }
}
