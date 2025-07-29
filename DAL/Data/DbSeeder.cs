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


        // ID của các EVENT
        private static readonly Guid Event1 = Guid.Parse("A1B2C3D4-E5F6-7890-1234-56789ABCDEF1");
        private static readonly Guid Event2 = Guid.Parse("A1B2C3D4-E5F6-7890-1234-56789ABCDEF2");
        private static readonly Guid Event3 = Guid.Parse("A1B2C3D4-E5F6-7890-1234-56789ABCDEF3");
        private static readonly Guid Event4 = Guid.Parse("A1B2C3D4-E5F6-7890-1234-56789ABCDEF4");
        private static readonly Guid Event5 = Guid.Parse("A1B2C3D4-E5F6-7890-1234-56789ABCDEF5");

        //USER
        private static readonly Guid UserId = Guid.Parse("C5D6E7F8-9A0B-1C2D-3E4F-5A6B7C8D9E0F");
        private static readonly Guid HospitalId = Guid.Parse("B4567890-ABCD-EF12-3456-7890ABCDEFAC");
        private static readonly Guid StaffId = Guid.Parse("AAAAAAAA-BBBB-CCCC-DDDD-EEEEEEEEEEEE");

        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedChronicDiseases(modelBuilder);
            SeedUser(modelBuilder);
            SeedDonationEvents(modelBuilder);

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
                        Password = fixedHashedPassword,
                        Role = Common.Enum.RoleType.CUSTOMER

                    },
                    new User
                    {
                        UserId = HospitalId,
                        UserName = "Hospital",
                        Email = "dungttse160616@fpt.edu.vn",
                        Password = fixedHashedPassword,
                        Role = Common.Enum.RoleType.HOSPITAL
                    },
                    new User
                    {
                        UserId = StaffId,
                        UserName = "Staff",
                        Email = "staff@gmail.com",
                        Password = fixedHashedPassword,
                        Role = Common.Enum.RoleType.STAFF
                    }
                );
        }

        private static void SeedDonationEvents(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonationEvent>().HasData(
                new DonationEvent
                {
                    DonationEventId = Event1,
                    Title = "Sự kiện hiến máu nhân đạo 1",
                    Location = "Bệnh viện Chợ Rẫy",
                    StartTime = new DateTime(2025, 7, 1, 8, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 7, 1, 12, 0, 0, DateTimeKind.Utc),
                    Description = "Sự kiện hiến máu dành cho cộng đồng tại Bệnh viện Chợ Rẫy."
                },
                new DonationEvent
                {
                    DonationEventId = Event2,
                    Title = "Ngày hội hiến máu 2",
                    Location = "Đại học Y Dược TP.HCM",
                    StartTime = new DateTime(2025, 7, 3, 9, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 7, 3, 14, 0, 0, DateTimeKind.Utc),
                    Description = "Chương trình hiến máu tình nguyện cho sinh viên và cán bộ trường."
                },
                new DonationEvent
                {
                    DonationEventId = Event3,
                    Title = "Hiến máu cứu người 3",
                    Location = "Nhà văn hóa Thanh Niên",
                    StartTime = new DateTime(2025, 7, 7, 8, 30, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 7, 7, 14, 30, 0, DateTimeKind.Utc),
                    Description = "Sự kiện hiến máu mở rộng cho mọi đối tượng tham gia."
                },
                new DonationEvent
                {
                    DonationEventId = Event4,
                    Title = "Giọt hồng nhân ái 4",
                    Location = "Công viên Lê Văn Tám",
                    StartTime = new DateTime(2025, 7, 14, 7, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 7, 14, 10, 0, 0, DateTimeKind.Utc),
                    Description = "Ngày hội hiến máu do Đoàn Thanh niên tổ chức."
                },
                new DonationEvent
                {
                    DonationEventId = Event5,
                    Title = "Ngày hội đỏ 5",
                    Location = "Trung tâm Hội nghị Quốc gia",
                    StartTime = new DateTime(2025, 7, 21, 8, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 7, 21, 16, 0, 0, DateTimeKind.Utc),
                    Description = "Sự kiện hiến máu lớn nhất năm dành cho toàn thành phố."
                }
            );
        }


    }
}
