using System;
using DMMInterface;
using NationalInstruments.VisaNS;

namespace AgilentDMM
{
    public class _34401A : IDMM
    {
        private _DC dc = null;
        private _AC ac = null;
        private _Resistance resistance = null;
        private MessageBasedSession lSession = null;

        public _34401A(string address)
        {
            string resource = "";
            try
            {
                resource = "GPIB::" + address + "::INSTR";
                // get session
                lSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(resource);

                if(lSession == null)
                    throw new Exception("");
            }
            catch(Exception ex)
            {
                throw new Exception("Instrument not present.");
            }
        }

        public _34401A(MessageBasedSession session)
        {
            lSession = session;
        }

        void IDMM.Close()
        {
            lSession.Dispose();
        }

        public IDMM.IDC DC
        {
            get
            {
                if (dc == null)
                    dc = new _DC(lSession);
                return dc;
            }
        }

        public IDMM.IAC AC
        {
            get
            {
                if (ac == null)
                    ac = new _AC(lSession);
                return ac;
            }
        }

        public IDMM.IResistance Resistance
        {
            get
            {
                if (resistance == null)
                    resistance = new _Resistance(lSession);
                return resistance;
            }
        }

        string IDMM.ID
        {
            get
            {
                string id = "";
                // get ID of DMM
                try
                {
                    id = lSession.Query("*IDN?");
                }
                catch
                {
                    id = "ERROR";
                }
                return id;
            }
        }

        string IDMM.err
        {
            get
            {
                
                string err = "";

                // get last error from DMM
                try
                {
                    err = lSession.Query("SYST:ERR?");
                }
                catch
                {
                    err = "ERROR";
                }
                return err;
            }
        }

        void IDMM.Reset()
        {
            // send command to reset DMM
            try
            {
                lSession.Write("*RST");
            }
            catch(Exception ex)
            {

            }
        }

        public class _DC:IDMM.IDC
        {
            MessageBasedSession lSession = null;
            public _DC(MessageBasedSession session)
            {
                lSession = session;
            }

            private _Voltage voltage  = null;
            private _Current current = null;


            public IDMM.IDC.IVoltage Voltage
            {
                get
                {
                    if (voltage == null)
                        voltage = new _Voltage(lSession);
                    return voltage;
                }
            }

            public IDMM.IDC.ICurrent Current
            {
                get
                {
                    if (current == null)
                        current = new _Current(lSession);
                    return current;
                }
            }

            public class _Voltage : IDMM.IDC.IVoltage
            {
                MessageBasedSession lSession = null;
                IDMM.IDC.IVoltage.RangeEnum DCRange = IDMM.IDC.IVoltage.RangeEnum.AUTO;

                public _Voltage(MessageBasedSession session)
                {
                    lSession = session;
                }

                string err
                {
                    get
                    {
                        string err = "";

                        // get last error from DMM
                        try
                        {
                            err = lSession.Query("SYST:ERR?");
                        }
                        catch
                        {
                            err = "ERROR";
                        }
                        return err;
                    }
                }

                public IDMM.IDC.IVoltage.RangeEnum Range
                {
                    get
                    {
                        return DCRange;
                    }

                    set
                    {
                        string rangeStr = "AUTO";
                        switch (value)
                        {
                            case IDMM.IDC.IVoltage.RangeEnum.AUTO:
                                rangeStr = "AUTO,0.001";
                                break;
                            case IDMM.IDC.IVoltage.RangeEnum._1000:
                                rangeStr = "1000,0.001";
                                break;
                            case IDMM.IDC.IVoltage.RangeEnum._100m:
                                rangeStr = ".100,0.001";
                                break;
                            case IDMM.IDC.IVoltage.RangeEnum._100:
                                rangeStr = "100,0.001";
                                break;
                            case IDMM.IDC.IVoltage.RangeEnum._10:
                                rangeStr = "10,0.001";
                                break;
                            case IDMM.IDC.IVoltage.RangeEnum._1:
                                rangeStr = "1,0.001";
                                break;
                            default:
                                break;

                        }

                        string cmd = "CONF:VOLT:DC " + rangeStr;
                        lSession.Write(cmd);

                        if (err != "")
                            throw new Exception("Instrument error for " + cmd + "- " + err);
                    }
                }

                public double measure()
                {
                    double meas = 0;
                    if (double.TryParse(lSession.Query("MEAS:VOLT:DC?"), out meas))
                        return meas;
                    else
                        throw new Exception("DMM.DC.Voltage measurement error.");
 
                }
            }
            public class _Current : IDMM.IDC.ICurrent
            {
                IDMM.IDC.ICurrent.RangeEnum DCRange = IDMM.IDC.ICurrent.RangeEnum.AUTO;
                MessageBasedSession lSession = null;
                public _Current(MessageBasedSession session)
                {
                    lSession = session;
                }
                string err
                {
                    get
                    {
                        string err = "";

                        // get last error from DMM
                        try
                        {
                            err = lSession.Query("SYST:ERR?");
                        }
                        catch
                        {
                            err = "ERROR";
                        }
                        return err;
                    }
                }
                public IDMM.IDC.ICurrent.RangeEnum Range
                {
                    get
                    {
                        return DCRange;
                    }
                    set
                    {
                        string rangeStr = "AUTO";
                        switch (value)
                        {
                            case IDMM.IDC.ICurrent.RangeEnum.AUTO:
                                rangeStr = "AUTO,0.001";
                                break;
                            case IDMM.IDC.ICurrent.RangeEnum._10m:
                                rangeStr = ".01,0.001";
                                break;
                            case IDMM.IDC.ICurrent.RangeEnum._100m:
                                rangeStr = ".100,0.001";
                                break;
                            case IDMM.IDC.ICurrent.RangeEnum._1:
                                rangeStr = "1,0.001";
                                break;
                            case IDMM.IDC.ICurrent.RangeEnum._3:
                                rangeStr = "3,0.001";
                                break;
                            default:
                                break;

                        }

                        string cmd = "CONF:CURR:DC " + rangeStr;
                        lSession.Write(cmd);

                        if (err != "")
                            throw new Exception("Instrument error for " + cmd + "- " + err);
                    }
                }
                public double measure()
                {
                    double meas = 0;
                    if (double.TryParse(lSession.Query("MEAS:CURR:DC?"), out meas))
                        return meas;
                    else
                        throw new Exception("DMM.DC.Current measurement error.");
                }
            }
        }

        public class _AC:IDMM.IAC
        {
            private _Voltage voltage = null;
            private _Current current = null;
            MessageBasedSession lSession = null;

            public _AC(MessageBasedSession session)
            {
                lSession = session;
            }

            public IDMM.IAC.IVoltage Voltage
            {
                get
                {
                    if (voltage == null)
                        voltage = new _Voltage(lSession);
                    return voltage;
                }
            }

            public IDMM.IAC.ICurrent Current
            {
                get
                {
                    if (current == null)
                        current = new _Current(lSession);
                    return current;
                }
            }

            public class _Voltage : IDMM.IAC.IVoltage
            {
                IDMM.IAC.IVoltage.RangeEnum ACRange = IDMM.IAC.IVoltage.RangeEnum.AUTO;
                MessageBasedSession lSession = null;
                public _Voltage(MessageBasedSession session)
                {
                    lSession = session;
                }
                string err
                {
                    get
                    {
                        string err = "";

                        // get last error from DMM
                        try
                        {
                            err = lSession.Query("SYST:ERR?");
                        }
                        catch
                        {
                            err = "ERROR";
                        }
                        return err;
                    }
                }

                public IDMM.IAC.IVoltage.RangeEnum Range
                {
                    get
                    {
                        return ACRange;
                    }
                    set
                    {
                        string rangeStr = "AUTO";
                        switch (value)
                        {
                            case IDMM.IAC.IVoltage.RangeEnum.AUTO:
                                rangeStr = "AUTO,0.001";
                                break;
                            case IDMM.IAC.IVoltage.RangeEnum._1:
                                rangeStr = "1,0.001";
                                break;
                            case IDMM.IAC.IVoltage.RangeEnum._10:
                                rangeStr = "10,0.001";
                                break;
                            case IDMM.IAC.IVoltage.RangeEnum._100:
                                rangeStr = "100,0.001";
                                break;
                            case IDMM.IAC.IVoltage.RangeEnum._1000:
                                rangeStr = "1000,0.001";
                                break;
                            default:
                                break;

                        }

                        string cmd = "CONF:VOLT:AC " + rangeStr;
                        lSession.Write(cmd);

                        if (err != "")
                            throw new Exception("Instrument error for " + cmd + "- " + err);
                    }
                }
                public double measure()
                {
                    double meas = 0;
                    if (double.TryParse(lSession.Query("MEAS:VOLT:AC?"), out meas))
                        return meas;
                    else
                        throw new Exception("DMM.AC.Voltage measurement error.");
                }
            }
            public class _Current : IDMM.IAC.ICurrent
            {
                IDMM.IAC.ICurrent.RangeEnum ACRange = IDMM.IAC.ICurrent.RangeEnum.AUTO;
                MessageBasedSession lSession = null;
                public _Current(MessageBasedSession session)
                {
                    lSession = session;
                }
                string err
                {
                    get
                    {
                        string err = "";

                        // get last error from DMM
                        try
                        {
                            err = lSession.Query("SYST:ERR?");
                        }
                        catch
                        {
                            err = "ERROR";
                        }
                        return err;
                    }
                }

                public IDMM.IAC.ICurrent.RangeEnum Range
                {
                    get
                    {
                        return ACRange;
                    }
                    set
                    {
                        string rangeStr = "AUTO";
                        switch (value)
                        {
                            case IDMM.IAC.ICurrent.RangeEnum.AUTO:
                                rangeStr = "AUTO,0.001";
                                break;
                            case IDMM.IAC.ICurrent.RangeEnum._1:
                                rangeStr = "1,0.001";
                                break;
                            case IDMM.IAC.ICurrent.RangeEnum._3:
                                rangeStr = "3,0.001";
                                break;
                            default:
                                break;

                        }

                        string cmd = "CONF:VOLT:DC " + rangeStr;
                        lSession.Write(cmd);

                        if (err != "")
                            throw new Exception("Instrument error for " + cmd + "- " + err);
                    }
                }
                public double measure()
                {
                    double meas = 0;
                    if (double.TryParse(lSession.Query("MEAS:CURR:AC?"), out meas))
                        return meas;
                    else
                        throw new Exception("DMM.AC.Current measurement error.");
                }
            }
        }

        public class _Resistance:IDMM.IResistance
        {
            private _2W _2w = null;
            private _4W _4w = null;
            MessageBasedSession lSession = null;
            public _Resistance(MessageBasedSession session)
            {
                lSession = session;
            }
            IDMM.IResistance.I2W IDMM.IResistance._2W
            {
                get
                {
                    if (_2w == null)
                        _2w = new _2W(lSession);
                    return _2w;
                }
            }

            IDMM.IResistance.I4W IDMM.IResistance._4W
            {
                get
                {
                    if (_4w == null)
                        _4w = new _4W(lSession);
                    return _4w;
                }
            }
            public class _2W : IDMM.IResistance.I2W
            {
                MessageBasedSession lSession = null;
                public _2W(MessageBasedSession session)
                {
                    lSession = session;
                }

                public void Zero()
                {
                    lSession.Write("ZERO:AUTO ONCE");
                }

                public IDMM.IResistance.I2W.RangeEnum Range => throw new NotImplementedException();

                IDMM.IResistance.I2W.RangeEnum IDMM.IResistance.I2W.Range { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

                public double measure()
                {
                    throw new NotImplementedException();
                }

                double IDMM.IResistance.I2W.measure()
                {
                    throw new NotImplementedException();
                }
            }
            public class _4W : IDMM.IResistance.I4W
            {
                MessageBasedSession lSession = null;
                public _4W(MessageBasedSession session)
                {
                    lSession = session;
                }

                public IDMM.IResistance.I4W.RangeEnum Range => throw new NotImplementedException();

                IDMM.IResistance.I4W.RangeEnum IDMM.IResistance.I4W.Range { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

                public double measure()
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
