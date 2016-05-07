using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            swiatEntities swiat = new swiatEntities();

            #region Wypisz kraje których kod zaczyna się na literę 'p'

            //var query = from kraj in swiat.country
            //            where kraj.Code.StartsWith("p")
            //            select new
            //            {
            //                Country = kraj
            //            };

            //int i = 1;
            //foreach (var row in query)
            //{
            //    Console.WriteLine(i + " " + row.Country.Code + " " + row.Country.Name);
            //    i++;
            //}

            #endregion

            #region Wypisz średnią populację ludzi w krajach

            //var query = (from kraj in swiat.country
            //             where kraj.Code.StartsWith("p")
            //             select new
            //             {
            //                 Country = kraj
            //             }).Average(c => c.Country.Population);


            //Console.WriteLine("Średnia populacja na kraj to " + query);


            #endregion

            #region Wypisz kraje, których populacja wynosi +/- 2000000 od średniej
            //var avgPopulation = (from k in swiat.country select new { c = k }).Average(c => c.c.Population);
            //Console.WriteLine("średnia populacja " + avgPopulation);
            //Console.WriteLine();

            //var query = (from kraj in swiat.country
            //             where kraj.Population >= (from k in swiat.country select new { c = k }).Average(c => c.c.Population) - 2000000 &&
            //             kraj.Population <= (from k in swiat.country select new { c = k }).Average(c => c.c.Population) + 2000000
            //             select new
            //             {
            //                 Country = kraj
            //             });

            //int i = 1;
            //foreach (var row in query)
            //{
            //    Console.WriteLine(i + " " + row.Country.Name + " " + row.Country.Population);
            //    i++;
            //}

            #endregion



            #region Wypisz kraje z mniejszą populacją niż najludniejsze miasto świata
            //var city = (from miasto in swiat.city orderby miasto.Population descending select new { city = miasto }).FirstOrDefault();
            //Console.WriteLine("najludniejsze miasto " + city.city.Name + " " + city.city.Population);
            //Console.WriteLine();

            //var query = from kraj in swiat.country
            //            where kraj.Population < (from miasto in swiat.city orderby 
            //                                     miasto.Population descending
            //                                     select new { city = miasto }).
            //                                     FirstOrDefault().city.Population
            //            orderby kraj.Population descending
            //            select new
            //            {
            //                name = kraj.Name,
            //                population = kraj.Population
            //            };

            //int i = 1;
            //foreach (var row in query)
            //{
            //    Console.WriteLine(i + " " + row.name  + " " + row.population);
            //    i++;
            //}

            #endregion

            #region Wylicz wszystkie wystąpienia danego języka i wyświetl je w kolejności malejącej
            //var query = (from jezyk in swiat.countrylanguage
            //             group jezyk by jezyk.Language into grp

            //             select new
            //             {
            //                 luft = grp,
            //                 count = grp.Count()
            //             })//.OrderByDescending(c => c.count).Skip(1).Take(4);
                         ;
            //var query1 = from jezyk in swiat.countrylanguage
            //             group jezyk by jezyk.Language into grp

            //             select new
            //             {
            //                 luft = grp,
            //                 count = grp.Count()
            //             };
            //var query = query1.OrderByDescending(q => q.count);

            //int i = 1;
            //foreach (var row in query)
            //{
            //    Console.WriteLine(i + " " + row.luft.Key + " " + row.count);
            //    i++;
            //}

            //select countrylanguage.`Language`, count(countrylanguage.`Language`) from countrylanguage group by countrylanguage.`Language` order by 2 desc
            #endregion

            #region Wypisz kraje i ich stolice, w których jest więcej oficjalnych języków niz jeden
            //var query = from kraj in swiat.country
            //            join subquery in (from jezyk in swiat.countrylanguage
            //                              where jezyk.IsOfficial == "T"
            //                              group jezyk by jezyk.CountryCode into grp
            //                              where grp.Count() > 1

            //                              select new
            //                              {
            //                                  code = grp.Key,
            //                                  count = grp.Count()
            //                              }


            //           ) on kraj.Code equals subquery.code
            //            join miasto in swiat.city on kraj.Capital equals miasto.ID
            //            select new
            //            {
            //                Country = kraj,
            //                Capital = miasto,
            //                LanguageCount = subquery.count
            //            };

            //int i = 1;
            //foreach (var row in query)
            //{
            //    Console.WriteLine(i + " " + row.Country.Code + " " + row.Country.Name + "\t" + row.Capital.Name + "\tliczba oficjalnych języków " + row.LanguageCount);
            //    i++;
            //}
            //  select countrylanguage.CountryCode, country.Name, city.Name from countrylanguage, country, city
            //  where countrylanguage.CountryCode = country.Code and 
            //  countrylanguage.IsOfficial = "T" and country.Capital = city.ID
            //  group by 1 HAVING count(countrylanguage.CountryCode )>1

            #endregion
            #region Wypisz miasta, w których mówi się po angielsku i formą rządów jest monarchia konstytucyjna sortując po populacji miast
            var query = (from miasto in swiat.city
                         join kraj in swiat.country on miasto.CountryCode equals kraj.Code
                         join jezyk in swiat.countrylanguage on miasto.CountryCode equals jezyk.CountryCode
                         where jezyk.Language == "english" && kraj.GovernmentForm == "Constitutional Monarchy"
                         orderby miasto.Population descending
                         select new
                         {
                             City = miasto,
                             Country = kraj,
                             Language = jezyk
                         }).GroupBy(c => c.City.CountryCode);
                        ;

            int i = 1;
            foreach (var row in query)
            {
                Console.WriteLine(i + " " + row.Key);
                //Console.WriteLine(i + " " + row.Country.Code + " " + row.City.Name + " " + row.City.Population + " " + row.Language.Language + " " + row.Country.GovernmentForm);
                i++;
            }

            // select * from city, countrylanguage, country 
            // where city.CountryCode = countrylanguage.CountryCode && countrylanguage.`Language` = "english" 
            //    && countrylanguage.CountryCode = country.Code && country.GovernmentForm = "Constitutional Monarchy" 
            //    order by city.Population desc
            #endregion


            Console.ReadKey();
        }
    }
}
