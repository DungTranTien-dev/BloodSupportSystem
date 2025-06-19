using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Data
{
    public class DbSeeder
    {
        // GUID cố định cho các bệnh
        private static readonly Guid Diabetes = Guid.Parse("A1E2C3D4-5F6A-7B8C-9D0E-1F2A3B4C5D6E");
        private static readonly Guid Hypertension = Guid.Parse("B2D3E4F5-6A7B-8C9D-0E1F-2A3B4C5D6E7F");
        private static readonly Guid Cardiovascular = Guid.Parse("C3D4E5F6-7A8B-9C0D-1E2F-3A4B5C6D7E8F");
        private static readonly Guid Asthma = Guid.Parse("D4E5F6A7-8B9C-0D1E-2F3A-4B5C6D7E8F9A");
        private static readonly Guid KidneyDisease = Guid.Parse("E5F6A7B8-9C0D-1E2F-3A4B-5C6D7E8F9A0B");
        private static readonly Guid LiverDisease = Guid.Parse("F6A7B8C9-0D1E-2F3A-4B5C-6D7E8F9A0B1C");
        private static readonly Guid Cancer = Guid.Parse("A7B8C9D0-1E2F-3A4B-5C6D-7E8F9A0B1C2D");
        private static readonly Guid HIV = Guid.Parse("B8C9D0E1-2F3A-4B5C-6D7E-8F9A0B1C2D3E");
        private static readonly Guid Hepatitis = Guid.Parse("C9D0E1F2-3A4B-5C6D-7E8F-9A0B1C2D3E4F");
        private static readonly Guid Other = Guid.Parse("D0E1F2A3-4B5C-6D7E-8F9A-0B1C2D3E4F5A");

        //USER
        private static readonly Guid UserId = Guid.Parse("C5D6E7F8-9A0B-1C2D-3E4F-5A6B7C8D9E0F");

        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedChronicDiseases(modelBuilder);
            SeedUser(modelBuilder);

            
        }

        private static void SeedChronicDiseases(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChronicDisease>().HasData(
                new ChronicDisease { ChronicDiseaseId = Diabetes, ChronicDiseaseName = "Tiểu đường" },
                new ChronicDisease { ChronicDiseaseId = Hypertension, ChronicDiseaseName = "Cao huyết áp" },
                new ChronicDisease { ChronicDiseaseId = Cardiovascular, ChronicDiseaseName = "Bệnh tim mạch" },
                new ChronicDisease { ChronicDiseaseId = Asthma, ChronicDiseaseName = "Hen suyễn" },
                new ChronicDisease { ChronicDiseaseId = KidneyDisease, ChronicDiseaseName = "Bệnh thận" },
                new ChronicDisease { ChronicDiseaseId = LiverDisease, ChronicDiseaseName = "Bệnh gan" },
                new ChronicDisease { ChronicDiseaseId = Cancer, ChronicDiseaseName = "Ung thư" },
                new ChronicDisease { ChronicDiseaseId = HIV, ChronicDiseaseName = "HIV/AIDS" },
                new ChronicDisease { ChronicDiseaseId = Hepatitis, ChronicDiseaseName = "Viêm gan B/C" },
                new ChronicDisease { ChronicDiseaseId = Other, ChronicDiseaseName = "Khác" }
            );
        }

        private static void SeedUser (ModelBuilder modelBuilder)
        {
            //pass: 123
            string fixedHashedPassword = "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm";
            modelBuilder.Entity<User>().HasData
                (
                    new User
                    {
                        UserId = UserId,
                        UserName = "User",
                        Email = "user@gmail.com",
                        Password = fixedHashedPassword
                    }
                );
        }
    }
}
