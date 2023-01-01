using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHS.Infrastructure.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 589, DateTimeKind.Utc).AddTicks(9573))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(3831))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(4735))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 593, DateTimeKind.Utc).AddTicks(3405))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    VendorKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalFeedback = table.Column<int>(type: "int", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 594, DateTimeKind.Utc).AddTicks(3073))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryOfVendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(5016))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryOfVendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryOfVendor_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryOfVendor_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopId = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedbackFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedbackType = table.Column<int>(type: "int", nullable: true),
                    FeedbackObjectId = table.Column<int>(type: "int", nullable: true),
                    FeedbackDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(5916))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedback_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fortune",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    Descr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageBanner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(7496))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fortune", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fortune_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoyaltyProgram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgBannerUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(5364))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyProgram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoyaltyProgram_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSetup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    DatetimeStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeStart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    AttachFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(6584))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSetup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationSetup_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParentProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgBanner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: false),
                    StkUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Highlight = table.Column<int>(type: "int", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupCode = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(9868))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentProduct_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParentProduct_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RankOfUserSetting",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    PointOfSilverMember = table.Column<int>(type: "int", nullable: false, defaultValue: 1000),
                    PointOfGoldMember = table.Column<int>(type: "int", nullable: false, defaultValue: 2000),
                    PointOfDiamondMember = table.Column<int>(type: "int", nullable: false, defaultValue: 3000),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 593, DateTimeKind.Utc).AddTicks(1451))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankOfUserSetting", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_RankOfUserSetting_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFollowVendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    IsFollow = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 593, DateTimeKind.Utc).AddTicks(4424))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollowVendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFollowVendor_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFollowVendor_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsFingerprintLogin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsGetNotifications = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LangId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 594, DateTimeKind.Utc).AddTicks(2168))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserSettings_Language_LangId",
                        column: x => x.LangId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSettings_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSettings_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeedbackImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedbackId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(6600))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackImage_Feedback_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "Feedback",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FortuneDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FortuneId = table.Column<int>(type: "int", nullable: false),
                    Probability = table.Column<int>(type: "int", nullable: false),
                    Limit = table.Column<int>(type: "int", nullable: false),
                    QtyAvailable = table.Column<int>(type: "int", nullable: false),
                    FortuneType = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(8544))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FortuneDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FortuneDetail_Fortune_FortuneId",
                        column: x => x.FortuneId,
                        principalTable: "Fortune",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FortuneTurnAddOfUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FortuneId = table.Column<int>(type: "int", nullable: false),
                    TurnAdd = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(9186))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FortuneTurnAddOfUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FortuneTurnAddOfUser_Fortune_FortuneId",
                        column: x => x.FortuneId,
                        principalTable: "Fortune",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FortuneTurnAddOfUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FortuneTurnOfUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FortuneId = table.Column<int>(type: "int", nullable: false),
                    TurnTotal = table.Column<int>(type: "int", nullable: false),
                    TurnAvailable = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(9868))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FortuneTurnOfUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FortuneTurnOfUser_Fortune_FortuneId",
                        column: x => x.FortuneId,
                        principalTable: "Fortune",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FortuneTurnOfUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoyaltyProgramImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoyaltyProgramId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(6001))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyProgramImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoyaltyProgramImage_LoyaltyProgram_LoyaltyProgramId",
                        column: x => x.LoyaltyProgramId,
                        principalTable: "LoyaltyProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotifyMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationSetUpId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    DatetimeStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttachFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    SeenTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Seen = table.Column<bool>(type: "bit", nullable: false),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    FcmMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(8567))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifyMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotifyMessage_NotificationSetup_NotificationSetUpId",
                        column: x => x.NotificationSetUpId,
                        principalTable: "NotificationSetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotifyMessage_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgBannerURL = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ParentProductId = table.Column<int>(type: "int", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    UnitRate = table.Column<float>(type: "real", nullable: false),
                    Qty = table.Column<float>(type: "real", nullable: false, defaultValue: -1f),
                    IsPromotion = table.Column<bool>(type: "bit", nullable: false),
                    PromotionTag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tstamp = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(3174))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ParentProduct_ParentProductId",
                        column: x => x.ParentProductId,
                        principalTable: "ParentProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FortuneUserReward",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FortuneId = table.Column<int>(type: "int", nullable: false),
                    FortuneDetailId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(797))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FortuneUserReward", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FortuneUserReward_Fortune_FortuneId",
                        column: x => x.FortuneId,
                        principalTable: "Fortune",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FortuneUserReward_FortuneDetail_FortuneDetailId",
                        column: x => x.FortuneDetailId,
                        principalTable: "FortuneDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FortuneUserReward_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AttributesId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(376))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeValue_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeValue_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IsRemove = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsOrder = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(2715))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cart_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiftOfLoyalty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    FortuneId = table.Column<int>(type: "int", nullable: true),
                    LoyaltyProgramId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    Limit = table.Column<int>(type: "int", nullable: false),
                    QtyAvailable = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(3362))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftOfLoyalty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftOfLoyalty_Fortune_FortuneId",
                        column: x => x.FortuneId,
                        principalTable: "Fortune",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GiftOfLoyalty_LoyaltyProgram_LoyaltyProgramId",
                        column: x => x.LoyaltyProgramId,
                        principalTable: "LoyaltyProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiftOfLoyalty_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductForUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(3910))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductForUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductForUser_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductForUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ParentProductId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(4604))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_ParentProduct_ParentProductId",
                        column: x => x.ParentProductId,
                        principalTable: "ParentProduct",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductImage_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductParticipatingLoyalty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    LoyaltyProgramId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    AmountOfMoney = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(6672))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductParticipatingLoyalty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductParticipatingLoyalty_LoyaltyProgram_LoyaltyProgramId",
                        column: x => x.LoyaltyProgramId,
                        principalTable: "LoyaltyProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductParticipatingLoyalty_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PromotionalProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    ParentProductId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PercentPromo = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AmountPromo = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(8461))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionalProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionalProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiftOfUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GiftOfLoyaltyId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 591, DateTimeKind.Utc).AddTicks(4041))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftOfUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftOfUser_GiftOfLoyalty_GiftOfLoyaltyId",
                        column: x => x.GiftOfLoyaltyId,
                        principalTable: "GiftOfLoyalty",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GiftOfUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BarCodeOfProductParticipatingLoyalty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductParticipatingId = table.Column<int>(type: "int", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 590, DateTimeKind.Utc).AddTicks(1382))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarCodeOfProductParticipatingLoyalty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarCodeOfProductParticipatingLoyalty_ProductParticipatingLoyalty_ProductParticipatingId",
                        column: x => x.ProductParticipatingId,
                        principalTable: "ProductParticipatingLoyalty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointOfUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    LoyaltyProgramId = table.Column<int>(type: "int", nullable: true),
                    ProductParticipatingLoyaltyId = table.Column<int>(type: "int", nullable: true),
                    FortuneId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 9, 9, 6, 39, 12, 592, DateTimeKind.Utc).AddTicks(535))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointOfUser_Fortune_FortuneId",
                        column: x => x.FortuneId,
                        principalTable: "Fortune",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PointOfUser_LoyaltyProgram_LoyaltyProgramId",
                        column: x => x.LoyaltyProgramId,
                        principalTable: "LoyaltyProgram",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PointOfUser_ProductParticipatingLoyalty_ProductParticipatingLoyaltyId",
                        column: x => x.ProductParticipatingLoyaltyId,
                        principalTable: "ProductParticipatingLoyalty",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PointOfUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointOfUser_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValue_AttributesId",
                table: "AttributeValue",
                column: "AttributesId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValue_ProductId",
                table: "AttributeValue",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BarCodeOfProductParticipatingLoyalty_ProductParticipatingId",
                table: "BarCodeOfProductParticipatingLoyalty",
                column: "ProductParticipatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductId",
                table: "Cart",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryOfVendor_CategoryId",
                table: "CategoryOfVendor",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryOfVendor_VendorId",
                table: "CategoryOfVendor",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_UserId",
                table: "Feedback",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_VendorId",
                table: "Feedback",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackImage_FeedbackId",
                table: "FeedbackImage",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Fortune_VendorId",
                table: "Fortune",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_FortuneDetail_FortuneId",
                table: "FortuneDetail",
                column: "FortuneId");

            migrationBuilder.CreateIndex(
                name: "IX_FortuneTurnAddOfUser_FortuneId",
                table: "FortuneTurnAddOfUser",
                column: "FortuneId");

            migrationBuilder.CreateIndex(
                name: "IX_FortuneTurnAddOfUser_UserId",
                table: "FortuneTurnAddOfUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FortuneTurnOfUser_FortuneId",
                table: "FortuneTurnOfUser",
                column: "FortuneId");

            migrationBuilder.CreateIndex(
                name: "IX_FortuneTurnOfUser_UserId",
                table: "FortuneTurnOfUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FortuneUserReward_FortuneDetailId",
                table: "FortuneUserReward",
                column: "FortuneDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_FortuneUserReward_FortuneId",
                table: "FortuneUserReward",
                column: "FortuneId");

            migrationBuilder.CreateIndex(
                name: "IX_FortuneUserReward_UserId",
                table: "FortuneUserReward",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftOfLoyalty_FortuneId",
                table: "GiftOfLoyalty",
                column: "FortuneId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftOfLoyalty_LoyaltyProgramId",
                table: "GiftOfLoyalty",
                column: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftOfLoyalty_ProductId",
                table: "GiftOfLoyalty",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftOfUser_GiftOfLoyaltyId",
                table: "GiftOfUser",
                column: "GiftOfLoyaltyId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftOfUser_UserId",
                table: "GiftOfUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProgram_VendorId",
                table: "LoyaltyProgram",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProgramImage_LoyaltyProgramId",
                table: "LoyaltyProgramImage",
                column: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSetup_VendorId",
                table: "NotificationSetup",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyMessage_NotificationSetUpId",
                table: "NotifyMessage",
                column: "NotificationSetUpId");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyMessage_UserId",
                table: "NotifyMessage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentProduct_CategoryId",
                table: "ParentProduct",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentProduct_VendorId",
                table: "ParentProduct",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUser_FortuneId",
                table: "PointOfUser",
                column: "FortuneId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUser_LoyaltyProgramId",
                table: "PointOfUser",
                column: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUser_ProductParticipatingLoyaltyId",
                table: "PointOfUser",
                column: "ProductParticipatingLoyaltyId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUser_UserId",
                table: "PointOfUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PointOfUser_VendorId",
                table: "PointOfUser",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Barcode",
                table: "Product",
                column: "Barcode");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ParentProductId",
                table: "Product",
                column: "ParentProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductForUser_ProductId",
                table: "ProductForUser",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductForUser_UserId",
                table: "ProductForUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ParentProductId",
                table: "ProductImage",
                column: "ParentProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductParticipatingLoyalty_LoyaltyProgramId",
                table: "ProductParticipatingLoyalty",
                column: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductParticipatingLoyalty_ProductId",
                table: "ProductParticipatingLoyalty",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionalProduct_ProductId",
                table: "PromotionalProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowVendor_UserId",
                table: "UserFollowVendor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowVendor_VendorId",
                table: "UserFollowVendor",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_LangId",
                table: "UserSettings",
                column: "LangId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_VendorId",
                table: "UserSettings",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeValue");

            migrationBuilder.DropTable(
                name: "BarCodeOfProductParticipatingLoyalty");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "CategoryOfVendor");

            migrationBuilder.DropTable(
                name: "FeedbackImage");

            migrationBuilder.DropTable(
                name: "FortuneTurnAddOfUser");

            migrationBuilder.DropTable(
                name: "FortuneTurnOfUser");

            migrationBuilder.DropTable(
                name: "FortuneUserReward");

            migrationBuilder.DropTable(
                name: "GiftOfUser");

            migrationBuilder.DropTable(
                name: "LoyaltyProgramImage");

            migrationBuilder.DropTable(
                name: "NotifyMessage");

            migrationBuilder.DropTable(
                name: "PointOfUser");

            migrationBuilder.DropTable(
                name: "ProductForUser");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "PromotionalProduct");

            migrationBuilder.DropTable(
                name: "RankOfUserSetting");

            migrationBuilder.DropTable(
                name: "UserFollowVendor");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "FortuneDetail");

            migrationBuilder.DropTable(
                name: "GiftOfLoyalty");

            migrationBuilder.DropTable(
                name: "NotificationSetup");

            migrationBuilder.DropTable(
                name: "ProductParticipatingLoyalty");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Fortune");

            migrationBuilder.DropTable(
                name: "LoyaltyProgram");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ParentProduct");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Vendor");
        }
    }
}
