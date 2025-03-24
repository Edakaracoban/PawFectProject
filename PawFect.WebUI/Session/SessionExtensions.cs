using Newtonsoft.Json;

namespace PawFect.WebUI.Session
{
    public static class SessionExtensions
    {
        // Session'a JSON formatında nesne kaydetmek için
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value)); // Nesneyi JSON'a dönüştürüp session'a kaydediyoruz
        }
        // Session'dan JSON formatındaki nesneyi almak için
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key); // Session'dan JSON stringini alıyoruz
            return value == null ? default : JsonConvert.DeserializeObject<T>(value); // JSON'dan nesneye dönüştürüyoruz
        }
    }
}