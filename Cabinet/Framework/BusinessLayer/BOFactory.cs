﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Framework.BusinessLayer
{
    public class BOFactory
    {
        public static BOBase getInstance(BusinessContext context)
        {
            switch (context.request.business)
            {
                case ("workInstruction"):
                    return new WorkInstructionBO(context);
                case ("cabinet"):
                    return new CabinetBO(context);
                case ("eqptRoom"):
                    return new EqptRoomBO(context);
                case ("region"):
                    return new RegionBO(context);
                default:
                    string err = "BOFactory: no such business:" + context.request.business + ".";
                    throw new BOException(err);
            }
        }

    }
}
