using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;

namespace Benchmark2
{
    [Config(typeof(Config))]
    [BenchmarkCategory("Ok", "JsonParsing")]
    public class OriginalJilComparisonTests
    {
        private static readonly string MiddleClassData =
                "{\r\n  \"S1\": {\r\n    \"Prop1\": \"asdfasdfjklasjdfjas;ljf\",\r\n    \"Prop2\": \"asdfasdfjklasjdfjas;ljf\"\r\n  },\r\n  \"S2\": {\r\n    \"Prop3\": \"k;jkajsdfkjasjfkjasdjfjasdf\",\r\n    \"Prop4\": \"kasjdfja;sfjdjlkajdfjdlasjfjas;ldfjljdaslfj;ljasdf\"\r\n  },\r\n  \"Prop3\": \"kjllkjjasdjf;kajdfkjasd;fjdjskfj\",\r\n  \"Prop4\": \"kjllkjjasdjf;kajdfkjasd;fjdjskfj\"\r\n}";

        private static readonly string LargeClassData =
                "{\"abuse\":{\"type\":\"21\",\"objectid\":\"2f0500234f228801\",\"journal\":\"16113423423449670326\",\"disturber\":\"1550251060241697330\"},\"time\":1436025438,\"huid\":\"320a23432516000075b91003\",\"subevent\":{\"thread_id\":\"cd4419097cab05b44b641e1e02000000\",\"authors\":[{\"pic_50\":\"_______asdfgdsfgdsfdsfgbderfest.com/octobdesgsdfgsdfgdsfgsdfgsdfgdsfgsdgf592168\",\"video_count\":0,\"show_age\":1,\"has_photosafe\":0,\"nick\":\"\u041E\u043A\u0441\u0430\u043D\u0430\",\"is_friend\":0,\"mail\":\"dddddsdafasfasdfasst.com\",\"has_pic\":1,\"follower\":0,\"pic_big_filed\":\"_______filed7-11.my.octobderfest.com/picasdfasdfasdfdasfdasfdsadfafsadftobderfest.com%2Fasdfsdafdasf%2F_______nao.a%2F_avataasdf%3F1437592168&siasdf2acf2b104632ae21adsfasdfc06454e\",\"pic_190\":\"_______avt-15.foto.octobderfest.com/octobderfessdafsdafsaa/_avatar190?1437592168\",\"app_count\":{\"web\":0,\"mob_web\":0},\"following\":0,\"pic_32\":\"_______avt-20.foto.octobderfest.com/octobderfest/ikrdddd.a/_avatar32?1437592168\",\"last_visit\":null,\"uid\":\"1793923424999174349\",\"status_text\":\"asdfhjhljhsdkljfhkjhasdhfkhaskdfhkhasdkjhfksa\",\"pic_22\":\"_______avt-27.foto.octobderfest.com/octobderfdddddddddddddddavatar22?1437592168\",\"has_my\":1,\"age\":40,\"last_name\":\"asdf\u043Aasdf\",\"is_verified\":1,\"pic_big\":\"_______avt-9.foto.octobderfest.com/octobderfest/_______nao.a/_avatarbig?1437592168\",\"vip\":0,\"birthday\":\"16.03.1977\",\"link\":\"https://my.octobderfest.com/octobderfest/_______nao.a/\",\"pic_128\":\"_______avt-10.foto.octobderfest.com/octobderfest/_______nao.a/_avatar128?1437592168\",\"sex\":1,\"pic\":\"_______avt-6.foto.octobderfest.com/octobderfest/_______nao.a/_avatar?1437592168\",\"pic_small\":\"_______avt-7.foto.octobderfest.com/octobderfest/_______nao.a/_avatarsmall?1437592168\",\"groups_count\":0,\"pic_180\":\"_______avt-24.foto.octobderfest.com/octobderfest/_______nao.a/_avatar180?1437592168\",\"first_name\":\"kj;jkjljjjjjalsjdfasdfasdf\",\"pic_40\":\"_______avt-9.foto.octobderfest.com/octobderfest/_______nao.a/_avatar40?1437592168\"}],\"type_name\":\"micropost\",\"likes_count\":0,\"click_url\":\"_______my.octobderfest.com/octobderfest/ikrgggggggmicropollllllllllllllllllllltml\",\"abuse\":{\"type\":\"7\",\"objectid\":\"2f0btn00qwer8801\",\"journal\":\"16113468473449670326\",\"disturber\":\"17939asdf4999174349\"},\"time\":1436025292,\"huid\":\"cd4419092f0b00006bb98801\",\"user_text\":\"\u0423\u0440\u0430! \u041E\u0442\u043F\u0443\u0441\u043A\",\"is_liked_by_me\":0,\"subtype\":\"event\",\"is_commentable\":1,\"type\":\"3-23\",\"is_likeable\":1,\"id\":\"2f0b00006bb98801\"},\"subtype\":\"comment\",\"is_commentable\":0,\"id\":\"25160asdf5b91003\",\"is_likeable\":0}";
        [GlobalSetup]
        public void Init()
        {

            var o1 = Jil.JSON.Deserialize<MiddleClass>(MiddleClassData);
            var o11 = Jil.JSON.Deserialize<LargeClass>(LargeClassData);
            var o2 = JilFork.JSON.Deserialize<MiddleClass>(MiddleClassData);
            var o22 = JilFork.JSON.Deserialize<LargeClass>(LargeClassData);
        }
        [Benchmark]
        public object MiddleClassDeserializationWithJil()
        {
            return Jil.JSON.Deserialize<MiddleClass>(MiddleClassData);
        }
        [Benchmark]
        public object MiddleClassDeserializationWithJilFork()
        {
            return JilFork.JSON.Deserialize<MiddleClass>(MiddleClassData);
        }
        [Benchmark]
        public object LargeClassDeserializationWithJil()
        {
            return Jil.JSON.Deserialize<LargeClass>(LargeClassData);
        }
        [Benchmark]
        public object LargeClassDeserializationWithJilFork()
        {
            return JilFork.JSON.Deserialize<LargeClass>(LargeClassData);
        }
        public class MiddleClass
        {
            
        }
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(MemoryDiagnoser.Default);
            }
        }


    }
}