using RentCarWeb.Models;
using RentCarWeb.Data;

namespace RentCarWeb.Data.Seeders
{
    public static class DataSeeder
    {
        public static void SeedCars(RentCarDbContext context)
        {
            AddCarIfNotExists(context, new MsCar
            {
                Name = "Toyota Avanza",
                Model = "1.3 G",
                Year = 2022,
                License_Plate = "B 1234 XY",
                Number_Of_Car_Seats = 7,
                Transmission = "Manual",
                Price_Per_Day = 350000,
                Status = true,
                CarImages = new List<MsCarImages>
                {
                    new MsCarImages { Image_Link = "/images/cars/Avanza_Veloz.jpg" },
                    new MsCarImages { Image_Link = "/images/cars/Avanza_Veloz2.jpg" }
                }
            });

           
            AddCarIfNotExists(context, new MsCar
            {
                Name = "Daihatsu Ayla",
                Model = "Satya E",
                Year = 2023,
                License_Plate = "B 5678 ZZ",
                Number_Of_Car_Seats = 5,
                Transmission = "Automatic",
                Price_Per_Day = 300000,
                Status = true,
                CarImages = new List<MsCarImages>
                {
                    new MsCarImages { Image_Link = "/images/cars/Daihatsu_Ayla.jpg" },
                    new MsCarImages { Image_Link = "/images/cars/Daihatsu_Ayla2.jpg" }
                }
            });

            AddCarIfNotExists(context, new MsCar
            {
                Name = "Honda HR-V",
                Model = "E",
                Year = 2021,
                License_Plate = "B 9876 KL",
                Number_Of_Car_Seats = 5,
                Transmission = "Automatic",
                Price_Per_Day = 550000,
                Status = true,
                CarImages = new List<MsCarImages>
                {
                new MsCarImages { Image_Link = "/images/cars/Honda_Hrv.jpg" },
                new MsCarImages { Image_Link = "/images/cars/Honda_Hrv2.jpg" }
                }
            });

            AddCarIfNotExists(context, new MsCar
            {
                Name = "Mitsubishi Xpander",
                Model = "Exceed",
                Year = 2021,
                License_Plate = "A 9876 KJ",
                Number_Of_Car_Seats = 7,
                Transmission = "Automatic",
                Price_Per_Day = 450000,
                Status = true,
                CarImages = new List<MsCarImages>
                {
                new MsCarImages { Image_Link = "/images/cars/xpander.jpg" },
                new MsCarImages { Image_Link = "/images/cars/xpander2.jpg" }
                }
            });

            AddCarIfNotExists(context, new MsCar
            {
                Name = "Toyota Yaris",
                Model = "GR Sport",
                Year = 2024,
                License_Plate = "F 9876 AD",
                Number_Of_Car_Seats = 5,
                Transmission = "Automatic",
                Price_Per_Day = 500000,
                Status = true,
                CarImages = new List<MsCarImages>
                {
                new MsCarImages { Image_Link = "/images/cars/Toyota_Yaris.jpg" },
                new MsCarImages { Image_Link = "/images/cars/Toyota_Yaris2.jpg" }
                }
            });

            AddCarIfNotExists(context, new MsCar
            {
                Name = "Suzuki Ertiga",
                Model = "GX",
                Year = 2022,
                License_Plate = "B 1122 ER",
                Number_Of_Car_Seats = 7,
                Transmission = "Manual",
                Price_Per_Day = 400000,
                Status = true,
                CarImages = new List<MsCarImages>
                {
                new MsCarImages { Image_Link = "/images/cars/Suzuki_Ertiga.jpg" }
                }
            });

            AddCarIfNotExists(context, new MsCar
            {
                Name = "Honda Brio",
                Model = "RS",
                Year = 2023,
                License_Plate = "B 3344 BR",
                Number_Of_Car_Seats = 5,
                Transmission = "Automatic",
                Price_Per_Day = 320000,
                Status = true,
                CarImages = new List<MsCarImages>
                {
                new MsCarImages { Image_Link = "/images/cars/Honda_Brio.jpg" },
                new MsCarImages { Image_Link = "/images/cars/Honda_Brio2.jpg" }
                }
            });

            AddCarIfNotExists(context, new MsCar
            {
                Name = "Toyota Innova",
                Model = "Reborn G",
                Year = 2020,
                License_Plate = "D 5566 IN",
                Number_Of_Car_Seats = 7,
                Transmission = "Automatic",
                Price_Per_Day = 650000,
                Status = true,
                CarImages = new List<MsCarImages>
                {
                new MsCarImages { Image_Link = "/images/cars/Innova_Reborn.jpg" },
                new MsCarImages { Image_Link = "/images/cars/Innova_Reborn2.jpg" }
                }
            });

            AddCarIfNotExists(context, new MsCar
            {
                Name = "Hyundai Stargazer",
                Model = "Prime",
                Year = 2023,
                License_Plate = "B 7788 SG",
                Number_Of_Car_Seats = 7,
                Transmission = "Automatic",
                Price_Per_Day = 480000,
                Status = true,
                CarImages = new List<MsCarImages>
                {
                new MsCarImages { Image_Link = "/images/cars/Hyundai_Stargazer.jpg" },
                new MsCarImages { Image_Link = "/images/cars/Hyundai_Stargazer2.jpg" }
                }
            });

            context.SaveChanges();
        }

        private static void AddCarIfNotExists(RentCarDbContext context, MsCar car)
        {
            // Cegah duplikasi berdasarkan Nomor Plat
            var exists = context.MsCars
                .FirstOrDefault(c => c.License_Plate == car.License_Plate);

            if (exists == null)
            {
                context.MsCars.Add(car);
            }
        }
    }
}
