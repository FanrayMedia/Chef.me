using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chef.Profiles;

namespace Chef.IntegrationTests
{
    /// <summary>
    /// Seed some users manually.
    /// </summary>
    /// <remarks>
    /// The seven users below have been included in the Chef.sql, if you need more users you can 
    /// add them here.  The following users are hand-picked from wikipedia http://en.wikipedia.org/wiki/List_of_chefs 
    /// they are some of the famous chefs in history. 
    /// </remarks>
    [TestClass, Ignore]
    public class SeedUsers
    {
        ProfileService _service;

        [TestInitialize]
        public void SetUp()
        {
            _service = new ProfileService();
        }

        /// <summary>
        /// Seed 7 users.
        /// </summary>
        [TestMethod]
        public void Seeding_Users()
        {
            // http://en.wikipedia.org/wiki/Julia_Child
            _service.CreateUserProfile(new Profile()
            {
                UserName = "JuliaChild1912",
                Email = "JuliaChild1912@notset.com",
                FirstName = "Julia",
                LastName = "Child",
                Locations = "Pasadena, CA, USA",
                Headline = "American chef, author, and television personality",
                Bio = "She is recognized for bringing French cuisine to the American public with her debut cookbook, Mastering the Art of French Cooking, and her subsequent television programs, the most notable of which was The French Chef, which premiered in 1963.",
                BgImg = "A02.jpg",
                ProfileStyle = ProfileStyle.GetDefault(),
            });

            // http://en.wikipedia.org/wiki/Joyce_Chen
            _service.CreateUserProfile(new Profile()
            {
                UserName = "JoyceChen1917",
                Email = "JoyceChen1917@notset.com",
                FirstName = "Joyce",
                LastName = "Chen",
                Locations = "Beijing, China",
                Headline = "Chinese chef, restaurateur, author, television personality, and entrepreneur",
                Bio = "Joyce Chen was credited with popularizing northern-style Chinese cuisine in the United States, coining the name \"Peking Raviolis\" for potstickers, inventing and holding the patent to the flat bottom wok with handle (also known as a stir fry pan), and developing the first line of bottled Chinese stir fry sauces for the US market. Starting in 1958, she operated several popular Chinese restaurants in Cambridge, Massachusetts. She died of Alzheimer's disease in 1994. Four years after her death, Joyce Chen was included in the 1998 James Beard Foundation Hall of Fame. In 2012, the city of Cambridge held their first Central Square \"Festival of Dumplings\" in honor of Joyce Chen's birthday.",
                BgImg = "A03.jpg",
                ProfileStyle = ProfileStyle.GetDefault(),
            });

            // http://en.wikipedia.org/wiki/Guillaume_Tirel
            _service.CreateUserProfile(new Profile()
            {
                UserName = "GuillaumeTirel1310",
                Email = "GuillaumeTirel1310@notset.com",
                FirstName = "Guillaume",
                LastName = "Tirel",
                Locations = "Upper Normandy, France",
                Headline = "A.K.A Taillevent",
                Bio = "From 1326 he was queux, head chef, to Philip VI. In 1347, he became squire to the Dauphin de Viennois and his queux in 1349. In 1355 he became squire to the Duke of Normandy, in 1359 his queux and in 1361 his sergeant-at-arms.",
                BgImg = "A04.jpg",
                ProfileStyle = ProfileStyle.GetDefault(),
            });


            // http://en.wikipedia.org/wiki/Maestro_Martino_of_Como
            _service.CreateUserProfile(new Profile()
                {
                    UserName = "MaestroMartino1430",
                    Email = "MaestroMartino1430@notset.com",
                    FirstName = "Maestro",
                    LastName = "Martino",
                    Locations = "Torre, Blenio, Switzerland",
                    Headline = "The prince of cooks",
                    Bio = "Italian 15th-century culinary expert who was unequalled in his field at the time and could be considered the Western world's first celebrity chef.",
                    BgImg = "A05.jpg",
                    ProfileStyle = ProfileStyle.GetDefault(),
                });

            // http://en.wikipedia.org/wiki/Nicolas_Appert
            _service.CreateUserProfile(new Profile()
                {
                    UserName = "NicolasAppert1749",
                    Email = "NicolasAppert1749@notset.com",
                    FirstName = "Nicolas",
                    LastName = "Appert",
                    Locations = "Châlons-en-Champagne, France",
                    Headline = "The father of canning",
                    Bio = "He was the French inventor of airtight food preservation. Appert, known as the \"father of canning\", was a confectioner.",
                    BgImg = "A07.jpg",
                    ProfileStyle = ProfileStyle.GetDefault(),
                });

            // http://en.wikipedia.org/wiki/Marcel_Boulestin
            _service.CreateUserProfile(new Profile()
                {
                    UserName = "MarcelBoulestin1878",
                    Email = "MarcelBoulestin1878@notset.com",
                    FirstName = "Marcel",
                    LastName = "Boulestin",
                    Locations = "Poitiers, France",
                    Headline = "Perfect and récherché dinner to be found in all London",
                    Bio = "He was a French chef, restaurateur, and the author of cookery books that popularised French cuisine in the English-speaking world.",
                    BgImg = "A08.jpg",
                    ProfileStyle = ProfileStyle.GetDefault(),
                });

            // http://en.wikipedia.org/wiki/Alexis_Soyer
            _service.CreateUserProfile(new Profile()
                {
                    UserName = "AlexisSoyer1810",
                    Email = "AlexisSoyer1810@notset.com",
                    FirstName = "Alexis",
                    LastName = "Soyer",
                    Locations = "Meaux, France",
                    Headline = "The first celebrity chef",
                    Bio = "French chef who became the most celebrated cook in Victorian England and was arguably the first celebrity chef. He also tried to alleviate suffering of the Irish poor in the Great Irish Famine (1845–1849), and improve the food provided to British soldiers in the Crimean War.",
                    BgImg = "A09.jpg",
                    ProfileStyle = ProfileStyle.GetDefault(),
                });

        }
    }
}
