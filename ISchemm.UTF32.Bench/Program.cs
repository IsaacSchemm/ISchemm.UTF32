using ISchemm.UTF32;
using System;

namespace ISchemm.UTF32.Bench
{
    class Program
    {
        static void Main(string[] args)
        {
            string str1 = "The kererū (Hemiphaga novaeseelandiae) is a species of pigeon native to New Zealand. Described by Johann Friedrich Gmelin in 1789, it is a large, conspicuous pigeon, up to 50 cm (20 in) in length and ranging from 550 to 850 g (19 to 30 oz) in weight, with a white breast and iridescent green–blue plumage. Kererū pairs are monogamous, breeding over successive seasons and remaining together when not breeding. Found in a variety of habitats across the country, the kererū feeds mainly on fruits, as well as on leaves, buds and flowers. Its numbers have declined since European colonisation and the arrival of invasive mammals such as rats, stoats and possums, although its populations have recently increased in suburban habitats. Considered a taonga (cultural treasure) to the Māori people, the kererū was historically a major food source in Māori culture. In 2018, it was designated Bird of the Year by the New Zealand organisation Forest & Bird. (Full article...) ";
            string str2 = @"에드거 앨런 포(Edgar Allan Poe, 1809년 1월 19일 ~ 1849년 10월 7일)는 미국의 작가, 시인, 편집자, 문학평론가이다. 미국 낭만주의의 거두이자 미국 문학사 전체적으로 매우 중요하게 취급되는 작가이다. 미스터리 및 마카브레 작품들로 가장 유명하며, 미국 단편소설의 선구자이기도 하다. 또한 추리소설이라는 장르를 최초로 만들어냈다고 평가받으며, 나아가 과학소설 장르의 형성에 기여했다. 그는 오로지 저술과 집필을 통해서만 생활하려 한 미국 최초의 전업작가이며, 이 때문에 생전에 심한 재정난과 생활고를 겪으며 유년기를 제외한 평생을 불우하게 살았다.
포와 그의 작품은 미국 문학뿐 아니라 전 세계에 영향을 미쳤으며, 우주론과 암호학 같은 문학 외의 분야에도 직간접적인 영향을 미쳤다. 포의 작품들은 오늘날 문학, 음악, 영화를 막론하고 여러 대중문화에서 접할 수 있으며, 그의 생가 수 채가 박물관으로 지정 운영되고 있다. 전미 미스터리 작가상은 미스터리 장르에 포가 남긴 족적을 기념하여 매년 에드거 상이라는 상을 수여한다.";
            string str3 = @"وادي الملوك، ويعرف أيضا باسم ""وادي بيبان الملوك""، هو واد في مصر استخدم على مدار 500 سنة خلال الفترة ما بين القرنين السادس عشر والحادي عشر قبل الميلاد لتشييد مقابر لفراعنة ونبلاء الدولة الحديثة الممتدة خلال عصور الآسرات الثامنة عشر وحتى الأسرة العشرين بمصر القديمة، ويقع الوادي على الضفة الغربية لنهر النيل في مواجهة طيبة (الأقصر حاليا) بقلب مدينة طيبة الجنائزية القديمة. وينقسم وادي الملوك إلى واديين؛ الوادي الشرقي (حيث توجد أغلب المقابر الملكية) والوادي الغربي. وباكتشاف حجرة الدفن الأخيرة عام 2006 والمعروفة باسم المقبرة رقم 63، علاوة على اكتشاف مدخلين آخرين لنفس الحجرة خلال عام 2008، وصل عدد المقابر المكتشفة حتى الآن إلى 63 مقبرة متفاوتة الأحجام إذ تتراوح ما بين حفرة صغيرة في الأرض وحتى مقبرة معقدة التركيب تحوي أكثر من 120 حجرة دفن بداخلها بعض النبلاء ومن كان على علاقة بالأسرة الحاكمة في ذلك الوقت. وتعد هذه المنطقة مركزا للتنقيبات الكشفية لدراسة علم الآثار وعلم المصريات منذ نهاية القرن الثامن عشر إذ تثير مقابرها اهتمام الدارسين للتوسع في مثل هذه الدراسات والتنقيبات الأثرية. استخدمت هذه المقابر جميعها في دفن ملوك وأمراء الدولة الحديثة بمصر القديمة بالإضافة إلى بعض النبلاء ومن كان على علاقة بالأسرة الحاكمة في ذلك الوقت. وتتميز المقابر الملكية باحتوائها على رسومات ونقوش من الميثولوجيا المصرية القديمة توضح العقائد الدينية والمراسم التأبينية في ذلك الوقت. وجميع القبور المكتشفة قد تم فتحها ونهبها في العصور القديمة وعلى الرغم من ذلك بقت دليلا دامغا على قوة ورخاء ملوك ذلك الزمان. وقد ذاع صيت الوادي في العصر الحديث بعد اكتشاف مقبرة توت عنخ أمون كاملة وما دار حولها من أقاويل بخصوص لعنة الفراعنة، وظل الوادي مشتهرا بالتنقيبات الأثرية المنتشرة بين أرجائه حتى تم اعتماده كموقع للتراث العالمي عام 1979 بالإضافة إلى مدينة طيبة الجنائزية بأكملها. ";

            string[] arr = new[] { str1, str2, str3 };

            foreach (string str in arr)
            {
                String32.FromString(str).ToString();
                String32.FromString(str).Substring(50, 100).ToString();
                String32.FromString(str).Substring(200).ToString();
            }

            DateTime dt = DateTime.Now;
            for (int i = 0; i < 10_000; i++)
            {
                foreach (string str in arr)
                {
                    String32.FromString(str).ToString();
                    String32.FromString(str).Substring(50, 100).ToString();
                    String32.FromString(str).Substring(200).ToString();
                }
            }
            Console.WriteLine(DateTime.Now - dt);

            Console.WriteLine("Edgar Allan Poe");
            Console.WriteLine(String32.FromString(str2).Substring(9, 15));
        }
    }
}
