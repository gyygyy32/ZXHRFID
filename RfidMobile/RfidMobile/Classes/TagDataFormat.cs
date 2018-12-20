using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RfidMobile.Classes
{
    class TagDataFormat
    {
        public static byte[] CreateByteArray(ModuleInfo mi)
        {
            int year = DateTime.Parse(mi.PackedDate).Year;
            int month = DateTime.Parse(mi.PackedDate).Month;
            int day = DateTime.Parse(mi.PackedDate).Day;
            DateTime dateOfModulePacked = new DateTime(year, month, day, 0, 0, 0);

            year = DateTime.Parse(mi.CellDate).Year;
            month = DateTime.Parse(mi.CellDate).Month;
            day = DateTime.Parse(mi.CellDate).Day;
            DateTime celldate = new DateTime(year, month, day, 0, 0, 0);

            decimal iPmax = Decimal.Parse(mi.Pmax) * 100M;
            decimal iVoc = Decimal.Parse(mi.Voc) * 100M;
            decimal iIsc = Decimal.Parse(mi.Isc) * 100M;
            decimal iVpm = Decimal.Parse(mi.Vpm) * 100M;
            decimal iIpm = Decimal.Parse(mi.Ipm) * 100M;
            decimal ff = Decimal.Parse(mi.FF) * 100M;

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write("@@");
                    writer.Write(mi.customer);
                    writer.Write(mi.ProductType);
                    writer.Write(mi.Module_ID);
                    writer.Write(DateToInt16(dateOfModulePacked));
                    writer.Write((int)iPmax);
                    writer.Write((short)iVoc);
                    writer.Write((short)iIsc);
                    writer.Write((short)iVpm);
                    writer.Write((short)iIpm);
                    writer.Write((short)ff);

                    writer.Write(DateToInt16(celldate));

                    //writer.Write(mi.cell_supplier_country);
                    //writer.Write(mi.iec_date);
                    //writer.Write(mi.iec_verfy);
                    //writer.Write(mi.iso);
                    //writer.Write(mi.mfg_name);

                    writer.Write("##");
                    writer.Close();
                }
                return stream.ToArray();
            }
        }

        public static ModuleInfo ParserTagData(byte[] tagBuff)
        {
            try
            {
                MemoryStream memStream = new MemoryStream(tagBuff);
                BinaryReader buffReader = new BinaryReader(memStream);
                
                if (buffReader.ReadString() != "@@")
                {
                    throw new Exception("数据包开始标志出错");
                }
                string customer = buffReader.ReadString();
                string strProductType = buffReader.ReadString();
                string strModule_ID = buffReader.ReadString();
                DateTime packingDate = DateFormInt16(buffReader.ReadInt16());
                decimal dPmax = (decimal)buffReader.ReadInt32() / 100M;
                decimal dVoc = (decimal)buffReader.ReadInt16() / 100M;
                decimal dIsc = (decimal)buffReader.ReadInt16() / 100M;
                decimal dVpm = (decimal)buffReader.ReadInt16() / 100M;
                decimal dIpm = (decimal)buffReader.ReadInt16() / 100M;
                decimal ff = (decimal)buffReader.ReadInt16() / 100M;
                DateTime cellDate = DateFormInt16(buffReader.ReadInt16());

                //string s_cell_supplier_country = buffReader.ReadString();
                //string s_iec_date = buffReader.ReadString();
                //string s_iec_verfy = buffReader.ReadString();
                //string s_iso = buffReader.ReadString();
                //string s_mfg_name = buffReader.ReadString();

                string packetEnd = buffReader.ReadString();
                
                if (packetEnd != "##")
                {
                    throw new Exception("数据包结束标志出错");
                }
                else
                {
                    ModuleInfo rfidTag = new ModuleInfo();
                    rfidTag.customer = customer;
                    rfidTag.ProductType = strProductType;
                    rfidTag.Module_ID = strModule_ID;
                    rfidTag.PackedDate = packingDate.ToString("yyyy-MM-dd");
                    rfidTag.Pmax = dPmax.ToString();
                    rfidTag.Voc = dVoc.ToString();
                    rfidTag.Isc = dIsc.ToString();
                    rfidTag.Vpm = dVpm.ToString();
                    rfidTag.Ipm = dIpm.ToString();
                    rfidTag.CellDate = cellDate.ToString("yyyy-MM-dd");
                    rfidTag.FF = ff.ToString() + "%";
                    rfidTag.cell_supplier_country = "";
                    rfidTag.iec_date = "";
                    rfidTag.iec_verfy = "";
                    rfidTag.iso = "";
                    rfidTag.mfg_name = "";

                    return rfidTag;
                }


                //short length = buffReader.ReadInt16();
                //I_V_Point[] pointArray = new I_V_Point[length];
                //for (int i = 0; i < length; i++)
                //{
                //    pointArray[i] = new I_V_Point();
                //    pointArray[i].Current = buffReader.ReadInt32() * 1.0 / 10000000;
                //    pointArray[i].Voltage = buffReader.ReadInt32() * 1.0 / 10000000;
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("解析数据包出错：\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 当前日期减去基准日期相差的天数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static short DateToInt16(DateTime date)
        {
            TimeSpan span = date - DateTime.Parse("2016-01-01");
            int days = span.Days;
            return (short)days;
        }

        private static DateTime DateFormInt16(short days)
        {
            return DateTime.Parse("2016-01-01").AddDays(days);
        }
    }
}
