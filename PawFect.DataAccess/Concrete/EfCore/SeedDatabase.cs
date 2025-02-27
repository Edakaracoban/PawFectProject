using Microsoft.EntityFrameworkCore;
using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PawFect.DataAccess.Concrete.EfCore
{
    public class SeedDatabase // Webui deki program.cs  de tetiklenecektir.
    {
        public static void Seed() // bu method uygulama ilk kez çalıştığında seed veritabanı boşken test verileri eklemek için kullanılır.
        {
            var context = new DataContext();

            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                // Kategorileri ekle
                if (context.Categories.Count() == 0)
                {
                    context.AddRange(Categories);

                }

                // Ürünleri ekle
                if (context.Products.Count() == 0)
                {
                    var categories = context.Categories.ToList();

                    // Her ürün için CategoryId'yi ata
                    foreach (var product in Products)
                    {
                        var category = categories.FirstOrDefault(c => c.Name == product.Category.Name);
                        if (category != null)
                        {
                            product.CategoryId = category.Id;
                        }
                    }
                    context.AddRange(Products);
                   
                }
                if (context.Blogs.Count() == 0)
                {
                    context.AddRange(Blogs);
                    
                }
                context.SaveChanges();

            }
        }


        private static Blog[] Blogs =
        {
            new Blog
            {
                Image = "muhabbet-kuslarinda-ureme-ve-yumurtlama-845c.jpg",
                Header = "Muhabbet Kuşlarında Üreme ve Yumurtlama",
                Title = "Muhabbet kuşlarında üreme ve yumurtlama süreci, sağlıklı bir ortam ve uygun çiftleşme koşulları sağlandığında meydana gelir. Üreme döneminde dişi ve erkek kuşların davranışlarında bazı belirgin değişiklikler gözlemlenir. Muhabbet kuşlarında yumurtlama zamanı, kuşların olgunlaşması, çiftleşme politikaları ve uygun çevre koşullarına bağlıdır. Dişi muhabbet kuşları genellikle 6-12 aylık olduklarında üreme olgunluğuna erişirler. Bu yaşadıklarında yumurtlayabilirler, ancak en ideal çiftleşme yaşı 12-18 ay arasıdır. Erkek muhabbet kuşları da aynı yaşlarda çiftleşme olgunluğuna ulaşır, ancak genellikle dişiler kadar hızlı üremeyi engelleyenler.Yuva Kutusu : Dişi muhabbet kuşlarının yumurtlanabilmesi için kafeste uygun bir yuva kutusu bulunmalıdır. Yuva kutusu rahat, temiz ve güvenli olmalıdır. Işık ve Sıcaklık : Günlük ışık oluşumu 12 saat veya daha fazla olması, üreme değişimini tetikleyebilir. Ayrıca sıcaklık sıcaklığı da önemlidir; 18-25°C arası bir sıcaklık idealdir. Beslenme : Yumurtlama öncesi ve sırasında dişi kuşun yeterli miktarda protein alması gerekir. Yumurtlamaya hazırlık olarak anne, saklanan yumurta, miktar ve kalsiyum kaynağı olarak mürekkep balığı ruhsatı sunulmalıdır.",
                SubTitle = "Kuş"
            },
            new Blog
            {
                Image = "kopeklerde-kuaforun-onemi-5a2d.jpg",
                Header = "Köpeklerde Kuaförün Önemi",
                Title = "Köpeklerin tüylerinin bakımı, sağlıklı, temiz ve rahat olmaları için oldukça önemlidir. Düzenli tüy bakımı, sadece estetik açıdan değil, aynı zamanda köpeğinizin cilt sağlığı ve genel sağlığı için de gereklidir. Köpek tıraşı, yalnızca estetik bir işlem değil, köpeğin sağlığı ve genel performansı için son derece önemlidir. İşte köpek traşının önemini açıklayan bazı temel noktalar: Düzenli traş, köpeğin cildinin hava almasına olanak tanır ve cilt sağlığını korur. Uzayan ve dolaşan tüyler, derinin nefes almasını engelleyebilir. Tıraş sırasında köpeğin parazitleri gözlemlenebilir, dolayısıyla olası parazitler (pire, kene gibi) veya cilt sorunları erken fark edilebilir. Düzenli tıraş, köpeğin aşırı tüy dökmesini kontrol olarak alır. Bu, evde tüy miktarının sağlanması yardımcı olur ve köpeğinizin daha temiz kalmasını sağlar. Uzun ve kalıcı tüy yapısına sahip köpeklerin tüylerin dolaşması yaygın bir sorundur. Dolaşan tüyler bazı deri hastalıklarına neden olabilir. Düzenli traş, bu durumu engeller.Köpeğin düzenli olarak traş edilmesi, tüylerin arasında gizlenebilecek pire, kene gibi parazitlerin erken fark edilmesi yardımcı olur. ",
                SubTitle = "Köpek"
            },
            new Blog
            {
                Image = "sokakta-yavru-kedi-bulursan-yapman-gerek-72fb.jpg",
                Header = "Sokakta Yavru Kedi Bulursan Yapman Gerekenler",
                Title = "Sokakta yeni doğan bir kedi yavrusunu bulduysanız, onun annesini aramak için dikkatli olmak ve anneyi kaçırmadan hareket etmek önemlidir. İşte yapılması gereken adımlar. Doğru atılacak her adım yavru kedinin hayatta kalması için önemli kriterlerdir. Sokakta Yavru Kedi Bulursan Yapman Gerekenler. Kediler hakkında Sokakta yeni doğan bir kedi yavrusunu bulduysanız, onun annesini aramak için dikkatli olmak ve anneyi kaçırmadan hareket etmek önemlidir. İşte yapılması gereken adımlar. Doğru atılacak her adım yavru kedinin hayatta kalması için önemli kriterlerdir. 1.Değerlendirme​ Anneyi Arayın : Yavru kedinin annesi yakınlarda olabilir. Anne kedi genellikle yiyecek aramak için kısa süreliğine yavrunun yanından ayrılır. Yavruyu hemen almadan önce birkaç saat gözlemleyin. Yavrunun Durumunu Kontrol Edin : Üşüyorsa, zayıf görünüyorsa, susuz veya aç gibi görünüyorsa hemen müdahale edin.Yavru kediler ilk birkaç haftada kendi vücut ısılarını düzenleyemezler. Annesiz kalınması durumunda bu daha kritiktir. Yavrunun beden ısısını ölçerek ona yaşam şansı tanımak bizlerin asli görevlerindendir. İlk 2 hafta yavrularda beden ısısı 30-32°C , ardından 27-29°C derecelerde olmalıdır. Yavru kediyi bir kutuya veya taşıma çantasına koyarak, sıcak bir ortamı sağlamak şarttır. Ortamı sürekli kontrol ederek aşırı sıcak veya soğuk olmadığından emin olunmalıdır.",
                SubTitle = "Kedi"
            },
            new Blog
            {
                Image = "kemirgenler-icin-dogru-taban-malzemesi-s-9588.jpg",
                Header = "Kemirgenler İçin Doğru Taban Malzemesi Seçimi",
                Title = "Taban malzemesini seçerken, kemirgeninizin sağlık durumunu ve ihtiyaçlarını göz önünde bulundurarak tozsuz, emici, kokuyu hapseden ve zararlı kimyasallar içermeyen doğal malzemeleri tercih etmek en iyisidir. Kemirgenler için doğru taban malzemesi seçimi, onların sağlığı ve konforu açısından oldukça önemlidir. Seçilecek malzeme, kemirgenin türüne, büyüklüğüne ve çevresel ihtiyaçlarına göre belirlenmelidir. İşte yaygın kullanılan bazı taban malzemeleri ve özellikleri: Ahşap Peletler: Oldukça emici ve doğal bir malzemedir. Ancak biraz sert olduğu için kemirgenlerin rahat etmesi için üzerine yumuşak bir malzeme eklemek gerekebilir. Kağıt Peletler: Emiciliği yüksek, tozsuz ve yumuşaktır. Uygun Olduğu Türler: Genellikle tavşan ve büyük kemirgenler için daha uygundur. Çam veya Sedir Tabanlı Talaş: Bu tür malzemeler genellikle tavsiye edilmez, çünkü çam ve sedir talaşları kemirgenlerin solunum yollarına zarar verebilecek aromatik yağlar içerir. Gazete Kağıdı: Mürekkep içerdiği için kemirgenlerin sağlığına zarar verebilir. Aromalı veya Parfümlü Malzemeler: Kemirgenlerin hassas solunum sistemlerine zarar verebilir. Çam ve Sedir Talaşı: Alerjik reaksiyonlara ve solunum yolu sorunlarına neden olabilecek uçucu yağlar içerir.",
                SubTitle = "Hamster ve Tavşan"
            },
            new Blog
            {
                Image = "kedi-mamalarindaki-taurin-ve-taurinin-on-b1fe.jpg",
                Header = "Kemirgenler İçin Doğru Taban Malzemesi Seçimi",
                Title = "Kediler için taurinin önemi abartılamaz. Taurin, bir kedinin genel sağlığı ve refahı için çok önemli olan esansiyel bir amino asittir. Diğer hayvanlardan farklı olarak kediler taurini kendi başlarına sentezleyemezler ve onu diyetlerinden almak zorundadırlar. Taurin, bir kedinin görme ve kalp fonksiyonunda hayati bir rol oynar. Diyetlerinde yeterli miktarda taurin bulunmayan kedilerde körlük ve kalp hastalığı da dahil olmak üzere bir dizi sağlık sorunu yaşar. Aslında taurin eksikliği, kedi mamasına taurin eklemenin bu sağlık sorunlarını önleyebileceği keşfedilmeden önce bir zamanlar kedilerde önde gelen ölüm nedeniydi. Kedilerin taurin ihtiyaçlarını karşılamak için protein açısından zengin bir beslenmeye ihtiyaçları vardır. Birçok yüksek kaliteli kedi maması bu gereksinimleri karşılayacak şekilde formüle edilmiştir, ancak kedinizi beslediğiniz mamanın yeterli miktarda taurin içerdiğinden emin olmak önemlidir. Gastrointestinal bozukluklar gibi bazı kedi sağlığı koşullarının, kedinin diyetindeki taurini absorbe etme yeteneğini etkileyebileceğini de belirtmekte fayda vardır. Bu durumlarda diyetlerine ek taurin takviyesi yapılması gerekebilir. Taurin, bir kedinin yalnızca fiziksel sağlığını değil aynı zamanda zihinsel ve duygusal sağlığını da destekler. Gelişim için gereklidir. Bir kedi sahibi olarak, kedinizin mamasının içeriğine dikkat etmeniz ve onun bu temel amino asitten yeterince aldığından emin olmanız çok önemlidir.",
                SubTitle = "Kedi"
            }
        };




        private static Category[] Categories =
        {
            new Category(){Name="Köpek",Icon="dog.png"},
            new Category(){Name="Kedi",Icon="cat.png"},
            new Category(){Name="Kuş",Icon="bird.png"},
            new Category(){Name="Akvaryum",Icon="fish.png"},
            new Category(){Name="Hamster ve Tavşan",Icon="rabbit.png"}
        };

        private static Product[] Products = {
               new Product(){
                   Name = "Felicia Small Köpek Maması 3 Kg" ,
                   Price = 425m,
                   Image = "felicia-smallmini-dusuk-tahilli-kuzu-e-28d9-b.jpg" ,
                   Description ="Optimum düzeyde protein, yağ ve karbonhidrat dengesi ile mama, köpeğinize ömrü boyunca düzenli enerji ve iyi bir vücut yapısı temin etmek için formüle edilmiştir. Hassas sindirim sistemine sahip veya kuzunun mükemmel lezzetini tercih eden köpekler için idealdir.",
                   Stock=0,
                  Category = new Category() { Name = "Köpek" }
                },
                new Product(){
                    Name = "Royal Canin X-Small Köpek Maması 1,5 kg",
                    Price = 490m,
                    Image = "royal-canin-x-small-adult-yetiskin-min-e5-88d.jpg",
                    Description = "X-SMALL Adult , ağırlıkları 4 kg'a kadar olan çok küçük köpeklerin küçük çeneleri, yüksek enerji gereksinimleri ve kabızlığa olan hassasiyetleri gibi ortak özelliklerini destekleyen beslenme çözümleri sunar. Titizlikle oluşturulmuş bir Health Nutrition formülü, uzun ve sağlıklı bir yaşamın garantilenmesine yardımcı olabilir.",
                    Stock = 100,
                    Category = new Category() { Name = "Köpek" }
                },
                new Product(){
                    Name = "Dubex Kedi ve Köpekler İçin Oyun Alanı",
                    Price = 1200m,
                    Image = "dubex-kedi-ve-kopekler-icin-oyun-alani-5-d9de.jpg",
                    Description = "Köpekler için etrafı kapalı bir oyun alanı sunan Dubex Pet Park Kedi ve Köpekler İçin Oyun Alanı, güvenli şekilde vakit geçirebilmesi için evcil hayvanınızı belirli bir alanda tutar. İç ve dış mekanlarda kullanmaya uygun olan oyun alanı, tuvalet eğitim süreci için de son derece uygundur.",
                    Stock = 100,
                    Category = new Category() { Name = "Köpek" }
                },
                new Product(){
                    Name = "Hushpet Ultra Emici Köpek Külot Çiş Pedi L Beden 12'li",
                    Price = 310m,
                    Image = "hushpet-ultra-emici-kopek-kulot-cis-pe-ab9a-7.jpg",
                    Description = "Özellikle de engelli ya da hastalığa sahip olan köpeklerde tercih edilen Hush Pet Ultra Emici Köpek Külot Çiş Pedi, köpeklerin ameliyatlarından sonra da tercih edilebilen bir üründür. Bu sayede köpeğinizin ev içerisinde herhangi bir alana tuvaletini yapmasını engellemeniz mümkün hale gelir.",
                    Stock = 100,
                    Category = new Category() { Name = "Köpek" }
                },
                new Product(){
                    Name = "Pedigree Sığır Etli Konserve Köpek Maması 400 gr",
                    Price = 1635m,
                    Image = "pedigree-sigir-etli-konserve-kopek-mam-c28bc4.jpg",
                    Description = "Uzun yıllar yapılan araştırmalardan sonra, sağlıklı ve lezzetli yemeğin/mamanın sırrı iyi, doğal ve kaliteli malzeme kullanmak. Bu sağlıklı mama da taze etin kullanılmasının, etin hiçbir şekilde dondurulmamasına veya işlenmemesinin sonucudur.",
                    Stock = 100,
                    Category = new Category() { Name = "Köpek" }
                },
                new Product(){
                    Name = "Morando Dana Etli Pate Yetişkin Köpek Konservesi 400 gr",
                    Price = 1120m,
                    Image = "morando-dana-etli-pate-yetiskin-kopek--d5-bd0.jpg",
                    Description = "Uzun yıllar yapılan araştırmalardan sonra, sağlıklı ve lezzetli yemeğin/mamanın sırrı iyi, doğal ve kaliteli malzeme kullanmak. Bu sağlıklı mama da taze etin kullanılmasının, etin hiçbir şekilde dondurulmamasına veya işlenmemesinin sonucudur.",
                    Stock = 100,
                    Category = new Category() { Name = "Köpek" }
                },
                new Product(){
                    Name = "Flea Doctor Pilli Kedi Köpek Pire Tarağı",
                    Price = 320m,
                    Image = "flea-doctor-pilli-kedi-kopek-pire-tara--22d03.jpg",
                    Description = "Flea Doctor Pilli Kedi Köpek Pire Tarağı, kedi ve köpekler için özel tasarlanmıştır. Her ırktan ve yaştan kediler köpekler için uygundur. Pil ile çalışan bu özel pire tarağı yaydığı elektrik dalgaları sayesinde tarama yaptığınız alandaki parazitleri öldürecektir.",
                    Stock = 100,
                    Category = new Category() { Name = "Köpek" }
                },
                new Product(){
                    Name = "Lion Shark Köpek Can Yeleği XXL",
                    Price = 1050m,
                    Image = "lion-shark-kopek-can-yelegi-xxllion--4d80-.jpg",
                    Description = "Lion Shark Köpek Can Yeleği, köpeğinizin su üzerinde güvenliğini sağlayan ve denizde, gölette ya da diğer su aktivitelerinde kullanımı kolay olan bir güvenlik ürünüdür.Can yeleği genellikle parlak renklerde üretilir (örneğin turuncu, sarı), böylece köpeğiniz suyun içindeyken kolayca fark edilir.",
                    Stock = 100,
                    Category = new Category() { Name = "Köpek" }
                },
                new Product(){
                    Name = "New Food Kuzu Etli Yetişkin Köpek Maması 15 kg",
                    Price = 620m,
                    Image = "new-food-kuzu-etli-yetiskin-kopek-mama-cd854a.jpg",
                    Description = "New Food Kuzu Etli Yetişkin Köpek Maması'nın faydaları, köpeklerin sağlıklı bir şekilde beslenmesine yardımcı olacak özelliklerle doludur.Kuzu eti, köpeklerin kas yapısını destekleyen kaliteli bir protein kaynağıdır. Bu, kas gelişimini ve genel sağlığı artırır.",
                    Stock = 100,
                    Category = new Category() { Name = "Köpek" }
                },
                new Product(){
                    Name = "Zampa Bottas Köpek Arabası Haki 48x80x99 cm",
                    Price = 6840m,
                    Image = "zampa-bottas-kedi-kopek-arabasi-haki-4-c01d-f.jpg",
                    Description = "Zampa Bottas Kedi & Köpek Arabası, evcil hayvanlarınızı kolayca taşıyabileceğiniz, konforlu ve pratik bir taşıma aracıdır. Özellikle küçük ve orta boy kediler ile köpekler için tasarlanmıştır. Hem günlük gezilerde hem de veteriner ziyaretleri gibi özel durumlarda kullanılabilir.",
                    Stock = 0,
                    Category = new Category() { Name = "Köpek" }
                },
                new Product(){
                    Name = "Pro Plan Adult Somonlu Yetişkin Kedi Maması 1,5 kg",
                    Price = 540m,
                    Image = "pro-plan-adult-somonlu-yetiskin-kedi-m--9749-.jpg",
                    Description = "Yetişkin kediler için geliştirilen formülü genel olarak sağlığın korunması odaklıdır. Pro Plan Adult Somonlu tüyler, gözler, kas yapısı üzerinde birçok olumlu etkiye sahiptir. Pro Plan Adult Somonlu yüksek protein oranına sahiptir ve oldukça kolay sindirilebilir özelliktedir, maksimum sinirilebilme oranına sahip tanecikler daha az dışkı oluşmasını sağlar.",
                    Stock = 100,
                    Category = new Category() { Name = "Kedi" }
                },
                new Product(){
                    Name = "LindoCat Topaklaşan Lavantalı İnce Taneli Bentonit Kedi Kumu 10 lt",
                    Price = 325m,
                    Image = "lindocat-topaklasan-lavantali-ince-tan-5-ff36.jpg",
                    Description = "LindoCat Topaklaşan Lavantalı İnce Taneli Bentonit Kedi Kumu, bentonitlerden oluşan ince taneli bir kedi kumudur. Lavanta içeriği sayesinde kötü kokuların oluşumunu engeller.Ayrıca ortamdaki kötü kokuların azalmasına yardımcı olur, granül boyutu sebebi ile de ürün çok hızlı topaklaşma özelliği gösterir.",
                    Stock = 100,
                    Category = new Category() { Name = "Kedi" }
                },
                new Product(){
                    Name = "ProChoice Pro 32 Sterilised Tavuklu Kısır Kedi Maması 15 kg",
                    Price = 1840m,
                    Image = "prochoice-pro-32-sterilised-tavuklu-ki-cb983-.jpg",
                    Description = "Kısırlaştırılan kedilerin metabolizmalarında meydana gelen yavaşlama ve değişim sonucu beslenme rutinlerinin de değişmesi gerekir. Bu mama kısırlaşan yetişkin bir kedinin beslenmesine uygun olacak şekilde formüle edilmiştir. Yağ oranı ve kalori miktarı azaltılan dengeli bir mamadır. Bunun yanında sindirim sistemi için de oldukça faydalıdır.",
                    Stock = 100,
                    Category = new Category() { Name = "Kedi" }
                },
                new Product(){
                    Name = "Nutri Feline Tavuk ve Kuzu Etli Kedi Konservesi 85 gr",
                    Price = 1840m,
                    Image = "nutri-feline-tavuk-ve-kuzu-etli-kedi-k--4db1-.jpg",
                    Description = "Taze et çeşitliliği ile sevimli dostlarımızın doğal yaşantılarına uygun beslenmelerini sağlıklı ve güçlü olmalarını sağlar. Tahılsız içeriği sayesinde kedilerde kilo kontrolüne yardımcıdır. Tavuklu ve kuzu etli içeriği zengin protein kaynağı sağlar. Bu sayede kas kütlesinin korunumu, kas ve eklemlerin gelişimi sağlanabilir.",
                    Stock = 100,
                    Category = new Category() { Name = "Kedi" }
                },
                new Product(){
                    Name = "Reflex Sterilised Balıklı Kısırlaştırılmış Yetişkin Kuru Kedi Maması 15 kg",
                    Price = 1565m,
                    Image = "reflex-sterilised-balikli-kisirlastiri-9a-77f.jpg",
                    Description = "İçeriğindeki özel bir madde olan CitriStim ve bu gibi bileşenlerden ötürü kediniz için besleyicilikten ziyada bağışıklık güçlendirici bir mamadır. Keten tohumu sayesinde aynı zamanda sevimli dostunuzun deri ve tüy sağlığına da katkıda bulunur.",
                    Stock = 100,
                    Category = new Category() { Name = "Kedi" }
                },
                new Product(){
                    Name = "Catit Vesper Uzay Gemisi Kedi Tüneli Mavi",
                    Price = 2020m,
                    Image = "catit-vesper-uzay-gemisi-kedi-tuneli-m-0-b219.jpg",
                    Description = "Uzay roketi şeklindeki heyecan verici Catit Vesper Rocket kedi kulesi ile kediniz ayın üzerinde olacak. Kediler bu 3 seviyeli saklanma yeri ve şezlongunda oynarken ve keşfederken saatlerce eğlenecek. Yumuşak yastıklı, bir ipte peluş top oyuncak içerir.",
                    Stock = 100,
                    Category = new Category() { Name = "Kedi" }
                },
                new Product(){
                    Name = "Felix Multipack Somonlu ve Ton Balıklı Yetişkin Yaş Kedi Maması",
                    Price = 920m,
                    Image = "felix-multipack-somonlu-ve-ton-balikli-916-42.jpg",
                    Description = "Felix Multipack Somonlu ve Ton Balıklı Yetişkin Yaş Kedi Maması, kedinizin günlük beslenme ihtiyaçlarını karşılamak için yüksek kaliteli somon ve ton balığı içerir. Lezzetli, sindirimi kolay ve besleyici formülüyle, kedinizin sağlıklı bir şekilde beslenmesini sağlar.",
                    Stock = 100,
                    Category = new Category() { Name = "Kedi" }
                },
                new Product(){
                    Name = "Gourmet Perle Izgara Ton Balıklı Pouch Yetişkin Kedi Yaş Maması 85 gr",
                    Price = 700m,
                    Image = "gourmet-perle-izgara-ton-balikli-pouch-66a-ac.jpg",
                    Description = "Gourmet Perle Izgara Ton Balıklı Pouch Yetişkin Kedi Yaş Maması, kedinizin sağlıklı gelişimi için yüksek kaliteli ton balığı ve doğal içerikler sunar. Lezzetli, sindirimi kolay ve besleyici formülü ile kedinizin beslenme ihtiyaçlarını karşılar.",
                    Stock = 100,
                    Category = new Category() { Name = "Kedi" }
                },
                 new Product(){
                    Name = "Spectrum Low Grain Somonlu Hamsili ve Kızılcıklı Yetişkin Kedi Maması",
                    Price = 1850m,
                    Image = "spectrum-low-grain-somonlu-hamsili-ve--b723-4.jpg",
                    Description = "Spectrum Low Grain Somonlu Hamsili ve Kızılcıklı Yetişkin Kedi Maması, kedinizin sağlıklı bir şekilde beslenmesini sağlamak için düşük tahıllı formülü, yüksek kaliteli et kaynakları ve doğal antioksidanlar içerir. Sindirim sistemini desteklerken, bağışıklık sistemini güçlendirir ve sağlıklı tüyler için gerekli yağ asitlerini sunar.",
                    Stock = 0,
                    Category = new Category() { Name = "Kedi" }
                },
                 new Product(){
                    Name = "Trixie Tünel Kedi Oyuncağı 25x50 cm",
                    Price = 552m,
                    Image = "trixie-tunel-kedi-oyuncagi-25x50-cmtri-c2b7-4.jpg",
                    Description = "Trixie Kedi Tüneli, renkli ve ilgi çekici kumaşdan imal edilmiş kedi tüneli ile kediniz, eğlenceli saatler yaşar. Tünel içindeki hışırtılı folyo, kedinizi oyuna teşvik edecektir. Aynı zamanda yavru köpekler içinde kullanılabilir. Tünelin dış kısmı rengarenk ve puantiyelidir.",
                    Stock = 0,
                    Category = new Category() { Name = "Kedi" }
                },
                 new Product(){
                    Name = "Bİrdlife Karışık Renkli Kuş Kafesi 30 x 23 x 39 cm",
                    Price = 70m,
                    Image = "birdlife-karisik-renkli-kus-kafesi-30--65b0-4.jpg",
                    Description = "Birdlife Karışık Renkli Kuş Kafesi, üst düzey kalitede hammaddelerden üretilmiştir olup uzun süre kullanabileceğiniz bir üründür. Toksik madde içermemektedir. Hoş rengi ve şık tasarımlı görüntüsüyle kuşunuz yuvasını çok sevecektir.",
                    Stock = 100,
                    Category = new Category() { Name = "Kuş" }
                },
                 new Product(){
                    Name = "Versele Laga Loro Parque Amazon Papağan Yemi 1 Kg",
                    Price = 55m,
                    Image = "versele-laga-loro-parque-amazon-papaga-96bade.jpg",
                    Description = "Güney Amerika kökenli papağan türleri başta olmak üzere, tüm papağan türlerinin beğenerek yiyebileceği,  özel bir yem karışımıdır. Kuşun beğenerek yiyeceği tahıl patlakları, kabak çekirdeği, kuşburnu, kurutulmuş biber tohumu ve çam tohumu gibi tohumlar katılarak karışım zenginleştirilmiştir.",
                    Stock = 100,
                    Category = new Category() { Name = "Kuş" }
                },
                 new Product(){
                    Name = "Versele Laga Parrot Papağan Yemi 1 Kg",
                    Price = 88m,
                    Image = "versele-laga-parrot-papagan-yemi-1-kgv--bd9f-.jpg",
                    Description = "Ako, Ara Papağanı, Cacadu, Amazon vs papağanlar için tam, doğal içerikli mükemmel bir karışımdır. Papağanızın tüm besin ve vitamin ihtiyacına karşılık verir. Papağanınızın hayat kalitesini arttıran tüm besinsel değerlere sahiptir. Özel teknikle oksijeni alınarak paketlendiğinden, tazeliğini ve lezzetini son kullanma tarihine kadar koruyabilir.",
                    Stock = 100,
                    Category = new Category() { Name = "Kuş" }
                },
                 new Product(){
                    Name = "EuroGold Konuşturucu Yem Katkısı 150 gr",
                    Price = 95m,
                    Image = "eurogold-konusturucu-yem-katkisi-150-g-c1d137.jpg",
                    Description = "EuroGold Konuşturucu Yem Katkısı, özenle seçilmiş tahıl ve tohumlar ile vitamin ve mineral katkıları ile hazırlanmış özel bir yem katkısıdır. İçerdiği özel katkılar sayesinde kuşun enerjisini arttırıp, kuşun dilinin altında bulunan bezeleri yumuşatarak daha rahat konuşmasına yardımcı olur.",
                    Stock = 100,
                    Category = new Category() { Name = "Kuş" }
                },
                 new Product(){
                    Name = "Rotipet Mineralli Kuş Kumu Grit 300 gr",
                    Price = 15m,
                    Image = "rotipet-mineralli-kus-kumu-grit-300-gr-c567c4.jpg",
                    Description = "Rotipet Mineralli Kuş Kumu Grit, kuşlarınız için hazmettirici ve kalsiyum kaynağıdır. Rotipet kuş kumları kuşların midesinde/kursağında değirmen olarak diş gibi görev yapar. Kuşların hazmetmelerini kolaylaştırır. Ayrıca kuşlarınızın ihtiyacı olan kalsiyumu da içerir. Kuşlarınızın yemliğine her gün 1 çay kaşığı kadar ekleyiniz.",
                    Stock = 100,
                    Category = new Category() { Name = "Kuş" }
                },
                 new Product(){
                    Name = "Versele Laga Prestige Paraket Yemi 1 kg 3 Adet",
                    Price = 300m,
                    Image = "versele-laga-prestige-paraket-yemi-1-k-3d-b00.jpg",
                    Description = "Versele Laga Prestige Paraket Yemi, sultan papağanları ve paraketler için, tohumlar ve tahıllardan oluşan, tüm vitamin ve mineralleri içeriğinde barındıran, %100 doğal, sağlıklı karışık yemdir. Yemin özel içeriği kuşun tüylerinin sağlıklı ve parlak olmasını sağlar. Kuşun hayat kalitesini arttıracak olan tüm besinsel değerleri barındırır.",
                    Stock = 100,
                    Category = new Category() { Name = "Kuş" }
                },
                 new Product(){
                    Name = "Rotipet Kuş Kulesi",
                    Price = 50m,
                    Image = "rotipet-kus-kulesirotipet-2d-c3f.jpg",
                    Description = "Rotipet Kuş Kulesi, muhabbet kuşları, kanaryalar ve diğer egzotik kuş türleri için kafes içerisinde kullanılan kaliteli ahşap, kaliteli renkli boncuklar ve iplerden üretilmiş kuş oyuncağıdır.  Rotipet kuş oyuncaklarında kuşunuzun sağlığı düşünülerek yüksek kalite de malzemeler kullanılmıştır.",
                    Stock = 100,
                    Category = new Category() { Name = "Kuş" }
                },
                 new Product(){
                    Name = "Jungle Muhabbet Kuşu Yemi Poşet 1 kg",
                    Price = 30m,
                    Image = "jungle-muhabbet-kusu-yemi-poset-1-kgju-a5df80.jpg",
                    Description = "Jungle Muhabbet Kuşu Yemi , vitamin ve mineraller ile desteklenmiştir. Tam bir  beslenme sağlar. Özel paketleme tekniği havayı içeride hapsederek yemin bayatlama süresi uzatılmıştır. Ambalajlama esnasında kullanılan özel oksijen  tekniği ile ürünün son kullanma tarihine kadar taze kalmasını sağlar. Kuşların gereksinim duydukları tohumları içinde barındırır.",
                    Stock = 100,
                    Category = new Category() { Name = "Kuş" }
                },
                 new Product(){
                    Name = "Jungle Gurme Muhabbet Yemi 400 gr",
                    Price = 10m,
                    Image = "jungle-gurme-muhabbet-yemi-400-grjungl-6-6a96.jpg",
                    Description = "jungle Gurme Muhabbet Yemi, muhabbet kuşlarının beslenme alışkanlıkları dikkate alınarak özel olarak formüle edilmiştir. Özel paketleme tekniği havayı içeride hapsederek yemin bayatlama süresi uzatılmıştır. Kuşların gereksinim duydukları tüm tohumları içinde barındırır. % 100 doğaldır. Vitamin ve mineraller ile desteklenmiştir.",
                    Stock = 0,
                    Category = new Category() { Name = "Kuş" }
                },
                 new Product(){
                    Name = "Gold Wings Premium Meyveli Paraket Krakeri 2x65 gr",
                    Price = 65m,
                    Image = "gold-wings-premium-meyveli-paraket-kra-75e-42.jpg",
                    Description = "Muhabbet kuşları için özenle hazırlanmış meyveli kraker. İçeriğinde meyve kullanılarak zenginleştirilmiş, lezzetli ve severek  yedikleri mükemmel bir yem katkısıdır. Dostlarımızın ihtiyaç duydukları besinleri tam ve dengeli bir şekilde içerir.​ Ödül olarak verilebilir. Kuşlar birbirine yapışmış  olan yemleri yerken enerji sarf ederek formda kalır.",
                    Stock = 0,
                    Category = new Category() { Name = "Kuş" }
                },
                 new Product(){
                    Name = "JBL Pro AquaTest NO2 Nitrit Test Kiti (50 Testlik)",
                    Price = 220m,
                    Image = "jbl-pro-aquatest-no2-nitrit-test-kiti--444b62.jpg",
                    Description = "Akvaryum içerisinde bulunan suyun değeri balık ve bitkilerin sağlığı açısından büyük önem taşımaktadır. Bazen temiz görünen bir su içerisinde bile mikrop ve zararlı bakteri bulunabilir. Nitrit su içerisindeki bakteri dengesi bozulduğunda ya da yeni bir akvaryum kurulumunda ortaya çıkan zehirli bir azot türüdür.",
                    Stock =100,
                    Category = new Category() { Name = "Akvaryum" }
                },
                 new Product(){
                    Name = "Aquaforest - AF Carbon Boost 200ml Sıvı Karbon",
                    Price = 100m,
                    Image = "aquaforest-af-carbon-boost-200ml-sivi--25fe75.jpg",
                    Description = "Sulu bitkiler için tasarlanmış, profesyonelce kolayca emilebilen karbon takviyesi. Karbon, fotosentez için önemli bir substrat olan tüm organik bileşiklerin temel yapı taşıdır. Karbon yetersizliği, bitki gelişimini ve gelişimini inhibe eder. AF Carbon Boost'un düzenli kullanımı akvaryumunuzu iyi tutar ve çekici, sağlıklı bitki görünümü sağlar.",
                    Stock =100,
                    Category = new Category() { Name = "Akvaryum" }
                },
                 new Product(){
                    Name = "Sunsun Hw-603B Akvaryum Mini Dış Filtre",
                    Price = 305m,
                    Image = "sunsun-hw-603b-akvaryum-mini-dis-filtr-8a37-9.jpg",
                    Description = "Sunsun Dış Filtre güçlü motoru saatte 400 litre su kapasitesi ile 80 litrelik akvaryumlarda mekanik temizlik ve oksijen sağlar. Diğer filtrelerden farklı olarak dış filtreye su üstten girip alttan çıkmaktadır. Böylece su tüm filtre katmanlarını dolaşır. Suyun alttan çıkıp akvaryuma geri pompalanması için pompa filtre dışında çıkış borusuna koyulmuştur.",
                    Stock =100,
                    Category = new Category() { Name = "Akvaryum" }
                },
                 new Product(){
                    Name = "Marina Akvaryum Beta Başlangıç Seti Güneş Girdabı",
                    Price = 170m,
                    Image = "marina-beta-baslangic-seti-gunes-girda-d-43a5.jpg",
                    Description = "İlk kez Beta sahibi olmak isteyenler için tüm temel donanımları içerir. Şık tasarımı ile ofiste veya evde kullanabilirsiniz. Tasarımıyla ve görüntüsü ile günlük stresin atılmasında ideal bir ortam sağlar.Beta sahibi olmak isteyenler için özel olarak üretilmiştir, tüm temel donanımları içerir.",
                    Stock =100,
                    Category = new Category() { Name = "Akvaryum" }
                },
                 new Product(){
                    Name = "Marina Akvaryum Betta Seti 1,25 L Tower",
                    Price = 130m,
                    Image = "marina-betta-seti-125-l-towermarina-5ed-9f.jpg",
                    Description = "Marina Beta Seti,ilk kez beta sahibi olmak isteyenler için tüm temel donanımları içerir. Şık tasarımı ile ofiste veya evde kullanabilirsiniz. Tasarımıyla ve görüntüsü ile günlük stresin atılmasında ideal bir ortam sağlar.",
                    Stock =100,
                    Category = new Category() { Name = "Akvaryum" }
                },
                 new Product(){
                    Name = "JBL OxyTabs Oksijen Tabletleri 50 tablet",
                    Price = 60m,
                    Image = "jbl-oxytabs-oksijen-tabletleri-50-tabl-5-5caa.jpg",
                    Description = "Akvaryum balıkları için, elektrik kesintileri ve nakil sırasında ihtiyaç duydukları oksijeni sağlamak amacıyla kullanılan, oksijen tabletidir.Bir tablet 30 mg oksijen takviyesi sağlar.10 litre su için 1 tablet kullanılması tavsiye edilmektedir.",
                    Stock =100,
                    Category = new Category() { Name = "Akvaryum" }
                },
                 new Product(){
                    Name = "JBL Novo Bits Discus Ciklet Balık Yemi Kovadan Bölme 1000 gr",
                    Price = 80m,
                    Image = "jbl-novo-bits-discus-ciklet-balik-yemi-54-828.jpg",
                    Description = "İçeriğinde balıklarınızın canlı renklere sahip olmasını sağlayan Astaksantin ve Karotenoid maddeleri vardır. Böylelikle balıklarınızın rengi parlak ve canlı gözükür.Balıklarınızın rengini güzelleştiren bu yem suda herhangi bir kalıntı ya da renk bırakmaz.",
                    Stock =100,
                    Category = new Category() { Name = "Akvaryum" }
                },
                 new Product(){
                    Name = "Tetra Pond Koi Sticks Kırımızı Pond Balık Yemi 140 Gr 1000 Ml",
                    Price = 1000m,
                    Image = "tetra-pond-koi-sticks-kirimizi-pond-ba-428-f8.jpg",
                    Description = "Bahçe havuzlarında yaşayan tüm süs balıklar için stik şeklinde, yüzebilen ana yemdir. Vitaminlerle desteklenerek biyolojik şekilde hazırlanmış olan Tetra pond, beslenmenin yanı sıra balıkların genel sağlığını da destekler.Bütün süs havuzu balıkları için ideal günlük yemdir.",
                    Stock =0,
                    Category = new Category() { Name = "Akvaryum" }
                },
                 new Product(){
                    Name = "Tetra Discus Kova Balık Yemi 10 lt",
                    Price = 1200m,
                    Image = "tetra-discus-kova-balik-yemi-10-lttetr-1-425a.jpg",
                    Description = "Tetra Discus; granül formda özellikle Discus balıkları için üretilmiş ancak diğer balıklarında severek tükettiği bir ana yemdir. Renk, şekil ve besin değerlerine bakıldığında Discus balıklarının beslenme ihtiyaçlarına son derece uygundur. Tüm gerekli besinleri, vitaminleri ve iz elementleri içerir.",
                    Stock =0,
                    Category = new Category() { Name = "Akvaryum" }
                },
                 new Product(){
                    Name = "Versele Laga Cavia Nature GinePig İçin Tam Yem 700 gr",
                    Price = 55m,
                    Image = "versele-laga-cavia-nature-ginepig-icin-b-40d4.jpg",
                    Description = "Versele Laga Cavia Nature GinePig İçin Tam Yem, Guinea Pig, kobay ve büyük boy hamsterlar için, özel fermuarlı paketi sayesinde tamamen taze ve doğal aromasını da koruyan, vitamin ve mineraller ile zenginleştirilmiş, sebze ve meyve içeren, koku kontrollü pelet yemdir.  Tam ve dengeli bir beslenme sağlar. Tahıl içermeyen light formüllüdür.",
                    Stock =100,
                    Category = new Category() { Name = "Hamster ve Tavşan" }
                },
                 new Product(){
                    Name = "Habitrail Cristal Hamster Kafesi 26 x 41 x 24 cm",
                    Price = 840m,
                    Image = "habitrail-cristal-hamster-kafesi-26-x--68c328.jpg",
                    Description = "Habitrail Cristal, hamsterler için güvenli ve emniyetli bir merkezi yaşam alanı olarak hizmet veren ana habitat birimidir. Kafes hem dayanıklılık hem de mükemmel hava sirkülasyonu için plastikten yapılmıştır. Yuvarlatılmış köşelere sahip taban tasarımı, temizliği kolaylaştırır. Evcil hayvanınızı güvenli bir şekilde içeride tutmak için bir kilidi vardır.",
                    Stock =100,
                    Category = new Category() { Name = "Hamster ve Tavşan" }
                },
                 new Product(){
                    Name = "Habitrail Ovo Loft Hamster Kafesi",
                    Price = 1130m,
                    Image = "habitrail-ovo-loft-hamster-kafesihabit-6-490d.jpg",
                    Description = "Hamsterlerin ihtiyaçları için özel olarak tasarlanmış ideal ev ve oyun ortamıdır. Yaşam alanının kapsamlı boru sistemi, konektörler ve aksesuar kombinasyonu, birçok erişim noktası ve evcil hayvanınızla etkileşim için artırılmış fırsatlar sağlar. Net bileşenleri, evcil hayvan görünürlüğünü artırır. Yaşam alanı montajı ve temizliği kolaydır.",
                    Stock =100,
                    Category = new Category() { Name = "Hamster ve Tavşan" }
                },
                 new Product(){
                    Name = "Habitrail Ovo Studio Hamster Kafesi",
                    Price = 1030m,
                    Image = "habitrail-ovo-studio-hamster-kafesihab-e65b-c.jpg",
                    Description = "Hamsterlerin ihtiyaçları için özel olarak tasarlanmış ideal ev ve oyun ortamıdır. Yaşam alanının kapsamlı boru sistemi, konektörler ve aksesuar kombinasyonu, birçok erişim noktası ve evcil hayvanınızla etkileşim için artırılmış fırsatlar sağlar. Net bileşenleri, evcil hayvan görünürlüğünü artırır. Yaşam alanı montajı ve temizliği kolaydır.",
                    Stock =100,
                    Category = new Category() { Name = "Hamster ve Tavşan" }
                },
                 new Product(){
                    Name = "Verselelaga Crispy Muesli Hamster Yemi 400 gr",
                    Price = 17m,
                    Image = "verselelaga-crispy-muesli-hamster-yemi-c9d035.jpg",
                    Description = "Verselelaga Crispy Muesli Hamster Yemi, hamsterlar için özel fermuarlı paketi sayesinde tamamen taze ve doğal aromasını koruyan, vitaminler ve mineraller ile zenginleştirilmiş, fıstık ve çekirdek içerikli, karışık hamster yemidir. Hamsterınızın ihtiyacı olan vitamin ve mineralleri karşılar. Gün içinde daha enerjik olmasını sağlar.",
                    Stock =0,
                    Category = new Category() { Name = "Hamster ve Tavşan" }
                },
                 new Product(){
                    Name = "Garden Mix Platin Tavşan Yemi 1 kg",
                    Price = 14m,
                    Image = "garden-mix-platin-tavsan-yemi-1-kggard-166-e4.jpg",
                    Description = "Garden Mix Platin Tavşan Yemi, tavşanların aktife ve sağlıklı bir hayat sürmeleri için gerekli tüm besinleri içinde bulunduran, lif açısından zengin ve lezzetli bir yemdir  sindirimi düzenler ve tüy topaklarının oluşumunu önleyen kaliteli tavşan yemidir. Gardenmix tavşan yemi 6 çeşit içeriğin lezzetli karışımından oluşmaktadır.",
                    Stock =100,
                    Category = new Category() { Name = "Hamster ve Tavşan" }
                },
                 new Product(){
                    Name = "Versele Laga Nature Cuni 700 gr Tavşan Yemi",
                    Price = 107m,
                    Image = "versele-laga-nature-cuni-700-gr-tavsan--4b72-.jpg",
                    Description = "Tavşanlar için yüksek lifli karışık yemdir. Tahılsız, ekstra sebze içeriğine sahiptir.Ot, bitki ve sebze içeriği ile yüksek lif oranı yüksektir. Sağlıklı dişler için çıtır taneciklere sahiptir.Günlük kullanım miktarı: Tavşanın yaşına ve türüne göre değişiklik gösterebilir.",
                    Stock =100,
                    Category = new Category() { Name = "Hamster ve Tavşan" }
                },
                 new Product(){
                    Name = "Versele Laga Complete Cuni Adult Tavşan Yemi 500 Gr",
                    Price = 97m,
                    Image = "versele-laga-complete-cuni-adult-tavsa-627788.jpg",
                    Description = "Versele Laga Complete Cuni Adult Tavşan Yemi, yetişkin tavşanlar için, içerisinde havuç, zengin çim çeşitleri ve çayır otu bulunan, kötü kokuları azaltmaya yardımcı, bağırsak sağlığına faydalı, doğal diş sağlığını koruyan ve en iyi tazeliği veren tahılsız yetişkin tavşan pelet yemidir. İçerisinde bulunan yüksek besinler sayesinde sağlıklı yaşam sunar.",
                    Stock =100,
                    Category = new Category() { Name = "Hamster ve Tavşan" }
                 },
                 new Product(){
                    Name = "EuroGold Wild Kemirgenler İçin Doğal Mısır Koçanı Kemirgen Yemi 2'li",
                    Price = 97m,
                    Image = "eurogold-wild-kemirgenler-icin-dogal-m-b-d34d.jpg",
                    Description = "EuroGold Wild Kemirgenler İçin Doğal Mısır Koçanı Kemirgen Yemi, koçan mısır, kemirgenler için, tamamen doğal, mısır kemirgen yemidir. Kemirgen mısır koçanı % 100 doğaldır.Lifli içeriği sayesinde kemirgenlerinizin sindirim sistemini düzenler. Kemirgenlerinizin kolayca ulaşabileceği şekilde kafese asın. Haftada 1 ya da 2 defa verilmelidir.",
                    Stock =100,
                    Category = new Category() { Name = "Hamster ve Tavşan" }
                 },
                 new Product(){
                    Name = "Versele Laga Nature Cuni 700 gr Yavru Tavşan Yemi",
                    Price = 75m,
                    Image = "versele-laga-nature-cuni-700-gr-yavru--63f2ea.jpg",
                    Description = "Versele Laga Nature Cuni, yetişkin tavşanlar için yüksek lifli karışık yem. Tahılsız, ekstra sebze içeriği mevcuttur. Ot, bitki ve sebze içeriği ile yüksek lif oranı. Sağlıklı dişler için çıtır tanecikler. İçeriği çeşitli otlar ve sebzelerden oluşan yüksek lifli bir karışımdır. İçeriğindeKİ lifler, pancar ve bitki katkıları sindirimi düzenler.",
                    Stock =0,
                    Category = new Category() { Name = "Hamster ve Tavşan" }
                 },
        };
    }
}
