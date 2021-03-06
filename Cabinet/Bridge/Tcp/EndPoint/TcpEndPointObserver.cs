﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.Tcp.EndPoint
{
    public interface TcpEndPointObserver
    {
        void onTcpConnected(Guid sessionId);
        void onTcpDisconnected(Guid sessionId);
        void onTcpData(Guid sessionId, Descriptor descriptor);
        void onTcpError(Guid sessionId, string errorMessage);
    }
}
