-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: smarthome
-- ------------------------------------------------------
-- Server version	9.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `AspNetUsers`
--

DROP TABLE IF EXISTS `AspNetUsers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetUsers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  `City` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Hobby` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Mobile` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUsers`
--

LOCK TABLES `AspNetUsers` WRITE;
/*!40000 ALTER TABLE `AspNetUsers` DISABLE KEYS */;
INSERT INTO `AspNetUsers` VALUES ('2759ee1f-941f-453a-9370-9987bef7e14c','yechan.lee@udem.edu','YECHAN.LEE@UDEM.EDU','yechan.lee@udem.edu','YECHAN.LEE@UDEM.EDU',0,'AQAAAAIAAYagAAAAEChgl/UH40iS4HoBFTWmbbyViZUl747FH0G+Q/BiKfJ7W1II13yfXKFUS0aQI0ewBg==','XK3MSYALCX5VODML4E7NXKMV2ZKOJQBM','2cac4874-af69-4fe0-8e52-22a92a6bd991',NULL,0,0,NULL,1,0,'부산','스페인어','01048535172'),('f23469f7-3c1a-4764-ac7b-353906a13cee','yechan5172@nate.com','YECHAN5172@NATE.COM','yechan5172@nate.com','YECHAN5172@NATE.COM',0,'AQAAAAIAAYagAAAAENHvQ86bSD7zlG6vU19azheEZjEQiLVKrjlhvG97KLGk0C56n9rPp+DbF2CbeZWtKQ==','TTR2GVVUMWTRSH4G6IBCCAGCHLED7CO6','d67715fe-4b7e-463d-b226-f8a6247b57f0',NULL,0,0,NULL,1,0,'부산','스페인어','01048535172'),('ff323068-6a34-45ac-a9e5-1c62b83b5505','yechan5172@daum.net','YECHAN5172@DAUM.NET','yechan5172@daum.net','YECHAN5172@DAUM.NET',0,'AQAAAAIAAYagAAAAEHO/cp6WCpFC6n8tcPlo6lB+BUEjWnfvP08XTJoCYhkik2Ap02H9HavNJReXaVHmFA==','2A7XHHLKTE3K2DGRFSTLVKJQFYFRSM2V','1adfbbbf-253c-4df5-954c-a5b2a445b401',NULL,0,0,NULL,1,0,NULL,NULL,NULL);
/*!40000 ALTER TABLE `AspNetUsers` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-12 16:06:26
