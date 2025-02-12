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
                    context.SaveChanges();
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
                    context.SaveChanges();
                }
            }
        }

        private static Category[] Categories =
        {
            new Category(){Name="Köpek"},
            new Category(){Name="Kedi"},
            new Category(){Name="Kuş"},
            new Category(){Name="Akvaryum"},
            new Category(){Name="Hamster ve Tavşan"}
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






        };
    }
}
