using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMMInterface;
using NationalInstruments.VisaNS;

namespace Keysight
{
    public class _3458A : IDMM
    {
        private _DC dc = null;
        private _AC ac = null;
        private _Resistance resistance = null;
        private MessageBasedSession lSession = null;

        public _3458A(string address)
        {
            string resource = "";
            string cmd = "";
            try
            {
                if (System.IO.File.Exists(@"c:\temp\3458A.txt"))
                    System.IO.File.Delete(@"c:\temp\3458A.txt");

                writeStatus("3458A driver constructor.");
                resource = "GPIB::" + address + "::INSTR";
                // get session
                lSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(resource);

                lSession.Timeout = 10000;

                cmd = "*RST";
                lSession.Write(cmd);
                cmd = "PRESET NORM";
                lSession.Write(cmd);
                System.Threading.Thread.Sleep(100);
                cmd = "OFORMAT ASCII";
                lSession.Write(cmd);
                System.Threading.Thread.Sleep(100);
                cmd = "DCV 10";
                lSession.Write(cmd);
                System.Threading.Thread.Sleep(100);
                cmd = "TRIG AUTO";
                lSession.Write(cmd);
                System.Threading.Thread.Sleep(100);
                cmd = "NRDGS 1, 1"; //Set number of readings per trigger to desired value 
                lSession.Write(cmd);
                System.Threading.Thread.Sleep(100);
                cmd = "NPLC 200"; //Set desired number of NPLCs, increase for greater accuracy
                lSession.Write(cmd);
                System.Threading.Thread.Sleep(100);
                cmd = "MEM OFF";
                lSession.Write(cmd);
                System.Threading.Thread.Sleep(100);
                cmd = "END ALWAYS";
                lSession.Write(cmd);
                System.Threading.Thread.Sleep(100);
                string err = getError;

                //cmd = "TARM SGL";
                //lSession.Write(cmd);
                //System.Threading.Thread.Sleep(100);

                writeStatus("End of 3458A driver constructor.");
                if (lSession == null)
                    throw new Exception("");
            }
            catch (Exception ex)
            {
                writeStatus("Constructor: " + cmd + ": " + ex.Message);
                throw new Exception("Instrument not present.");
            }
        }

        void IDMM.Close()
        {
            lSession.Dispose();
        }

        string getError
        {
            get
            {
                string cmd = "ERR?";
                string error = lSession.Query(cmd).TrimEnd();
                writeStatus(error);
                return error;
            }
        }

        void writeStatus(string status)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\temp\3458A.txt", true);
            sw.WriteLine(status);
            sw.Close();
        }

        public _3458A(MessageBasedSession session)
        {
            lSession = session;
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
                    id = lSession.Query("ID?");
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
                    err = lSession.Query("ERR?");
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
            catch (Exception ex)
            {

            }
        }

        public class _DC : IDMM.IDC
        {
            MessageBasedSession lSession = null;
            public _DC(MessageBasedSession session)
            {
                lSession = session;
            }

            private _Voltage voltage = null;
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
                            err = lSession.Query("ERR?");
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

                void writeStatus(string status)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\temp\3458A.txt", true);
                    sw.WriteLine(status);
                    sw.Close();
                }

                public double measure()
                {
                    //lSession.TerminationCharacterEnabled = true;
                    //lSession.TerminationCharacter = Convert.ToByte('\n');

                    //lSession.Write("PRESET NORM");
                    //lSession.Write("OFORMAT ASCII");
                    //lSession.Write("DCV 10");
                    //lSession.Write("TRIG AUTO");
                    //lSession.Write("NRDGS 1, 1");  //Set number of readings per trigger to desired value 
                    //lSession.Write("NPLC 200"); //Set desired number of NPLCs, increase for greater accuracy 
                    //lSession.Write("TARM SGL");
                    //lSession.Write("END ALWAYS");
                    string cmd = "";

                    try
                    {
                        //cmd = "TARM 3, 1";
                        cmd = "TARM SGL";
                        lSession.Write(cmd);

                        cmd = "Read string";
                        string measStr = lSession.ReadString();
                        System.Threading.Thread.Sleep(5000);
                        double meas = 0;
                        if (double.TryParse(measStr, out meas))
                            return meas;
                        else
                            throw new Exception();
                    }
                    catch (Exception ex)
                    {
                        writeStatus(cmd + ": " + ex.Message);
                        throw new Exception(cmd + ": " + ex.Message);
                    }
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
                            err = lSession.Query("ERR?");
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

        public class _AC : IDMM.IAC
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
                            err = lSession.Query("ERR?");
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

        public class _Resistance : IDMM.IResistance
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

                double IDMM.IResistance.I4W.measure()
                {
                    throw new NotImplementedException();
                }
            }
        }
    }

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
                DeleteStatus();
                resource = "GPIB0::" + address + "::INSTR";
                WriteStatus("Create session: " + resource);
                // get session
                lSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(resource);

                if (lSession == null)
                    throw new Exception("");
            }
            catch (Exception ex)
            {
                WriteStatus("Exception: " + ex.Message);
                throw new Exception("Instrument not present. " + ex.Message);
            }
        }


        private void WriteStatus(string status)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\temp\ext_dmm.txt", true);
            sw.WriteLine(status);
            sw.Close();
        }

        private void DeleteStatus()
        {
            if (System.IO.File.Exists(@"c:\temp\ext_dmm.txt"))
                System.IO.File.Delete(@"c:\temp\ext_dmm.txt");
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
            catch (Exception ex)
            {

            }
        }

        public class _DC : IDMM.IDC
        {
            MessageBasedSession lSession = null;
            public _DC(MessageBasedSession session)
            {
                lSession = session;
            }

            private _Voltage voltage = null;
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

        public class _AC : IDMM.IAC
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

        public class _Resistance : IDMM.IResistance
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

