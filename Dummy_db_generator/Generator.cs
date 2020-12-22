using System;
using System.Text;

namespace Dummy_db_generator {
    public static class Generator {

        public static string GenInt(string size, Random random, long min = -9223372036854775808, long max = 9223372036854775807) {
            try {
                switch (size) {
                    case "TINYINT":
                        if (min == -9223372036854775808)
                            min = -128;
                        if (max == 9223372036854775807)
                            max = 127;
                        return random.Next((int)((min < -128) ? -128 : min), (int)((max > 127) ? 127 : max)).ToString();
                    case "SMALLINT":
                        if (min == -9223372036854775808)
                            min = -32768;
                        if (max == 9223372036854775807)
                            max = 32767;
                        return random.Next((int)((min < -32768) ? -32768 : min), (int)((max > 32767) ? 32767 : max)).ToString();
                    case "MEDIUMINT":
                        if (min == -9223372036854775808)
                            min = -8388608;
                        if (max == 9223372036854775807)
                            max = 8388607;
                        return random.Next((int)((min < -8388608) ? -8388608 : min), (int)((max > 8388607) ? 8388607 : max)).ToString();
                    case "INT":
                        if (min == -9223372036854775808)
                            min = -2147483648;
                        if (max == 9223372036854775807)
                            max = 2147483647;
                        return random.Next((int)((min < -2147483648) ? -2147483648 : min), (int)((max > 2147483647) ? 2147483647 : max)).ToString();
                    case "BIGINT":
                        long range = max - min;
                        long longRand;
                        do {
                            byte[] buf = new byte[8];
                            random.NextBytes(buf);
                            longRand = (long)BitConverter.ToInt64(buf, 0);
                        } while (longRand > long.MaxValue - ((long.MaxValue % range) + 1) % range);
                        longRand = (longRand % range) + min;

                        if (longRand % 5 < 0)
                            longRand = longRand * -1;
                        return longRand.ToString();
                    default:
                        return "42";
                }
            }
            catch (Exception e) {
                Console.WriteLine("Exception GenInt()" + e.Message + e.StackTrace);
            }
            return null;
        }

        public static string GenMail(Random random) {
            return $"{GenName("first", random)}.{GenName("last", random)}@mail.com";
        }

        public static string GenName(string type, Random random) {
            string name = "";
            try {
                if (type == "first")
                    name = StaticArray.firstNames[random.Next(StaticArray.firstNames.Length)];
                else if (type == "last")
                    name = StaticArray.lastNames[random.Next(StaticArray.lastNames.Length)];
            }
            catch (Exception e) {
                Console.WriteLine("Exception GenName()" + e.Message + e.StackTrace);
            }
            return name;
        }

        public static string GenDouble(int maxSize, int precision, double min, double max, Random random) {
            string result = "";
            try {
                double nextDouble = random.NextDouble() * (max - min) + min;

                if (nextDouble % 5 < 0)
                    nextDouble = nextDouble * -1;
                result = nextDouble.ToString();
                if (result.Length > maxSize)
                    result = result.Substring(0, result.Length - (result.Length - maxSize));
                else if (result.Split(',')[1].Length > precision)
                    result = result.Substring(0, result.Length - (result.Split('.')[1].Length - precision));
            }
            catch (Exception e) {
                Console.WriteLine("Exception GenDouble()" + e.Message + e.StackTrace);
            }
            return result;
        }

        public static string GenLorem(long maxSize, Random random) {
            StringBuilder result = new StringBuilder();
            try {

                byte[] buf = new byte[8];
                random.NextBytes(buf);
                long longRand = BitConverter.ToInt64(buf, 0);
                long randWord = (Math.Abs(longRand % (maxSize - 1)) + 1);               

                for (long w = 0; w < randWord; w++) {
                    if ((maxSize - result.Length) <= 13)
                        continue;
                    if (w > 0)
                        result.Append(" ");
                    result.Append(StaticArray.words[random.Next(StaticArray.words.Length)]);
                }
            }
            catch (Exception e) {
                Console.WriteLine("Exception GenLorem()" + e.Message + e.StackTrace);
            }

            return result.ToString();
        }

        public static string GenDateTime(bool date, bool time, Random random, bool futureDate, int maxYear) {
            DateTime dateTime = new DateTime();
            try {

                if (date) {
                    dateTime = dateTime.AddYears(random.Next(1900, futureDate ? maxYear : DateTime.Now.Year));
                    dateTime = dateTime.AddMonths(random.Next(1, futureDate ? 12 : DateTime.Now.Month));
                    dateTime = dateTime.AddDays(random.Next(1, futureDate ? 31 : DateTime.Now.Day));
                    if (!time)
                        return dateTime.ToShortDateString();
                }
                if (time) {
                    dateTime = dateTime.AddHours(random.Next(0, 24));
                    dateTime = dateTime.AddMinutes(random.Next(0, 60));
                    dateTime = dateTime.AddSeconds(random.Next(0, 60));
                    if (!date)
                        return dateTime.ToLongTimeString();
                }
            }
            catch (Exception e) {
                Console.WriteLine("Exception GenDateTime()" + e.Message + e.StackTrace);
            }

            return dateTime.ToString();
        }
    }
}
