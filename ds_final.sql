CREATE DATABASE  IF NOT EXISTS `db_final` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `db_final`;
-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: db_final
-- ------------------------------------------------------
-- Server version	8.0.34

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
-- Table structure for table `admin`
--

DROP TABLE IF EXISTS `admin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(100) NOT NULL,
  `password` varchar(255) NOT NULL,
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username_UNIQUE` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admin`
--

LOCK TABLES `admin` WRITE;
/*!40000 ALTER TABLE `admin` DISABLE KEYS */;
INSERT INTO `admin` VALUES (2,'Umar','$2a$11$EWD6969XztUipdDHcVRTbeGXC9IzkypW9nae2CyerS1QR5okNurFS','2026-05-25 19:29:58');
/*!40000 ALTER TABLE `admin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `api_sync_log`
--

DROP TABLE IF EXISTS `api_sync_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `api_sync_log` (
  `id` int NOT NULL AUTO_INCREMENT,
  `student_id` int DEFAULT NULL,
  `synced_at` datetime DEFAULT NULL,
  `status` enum('SUCCESS','FAILED') DEFAULT NULL,
  `message` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `sttd_id_idx` (`student_id`),
  CONSTRAINT `sttd_id` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `api_sync_log`
--

LOCK TABLES `api_sync_log` WRITE;
/*!40000 ALTER TABLE `api_sync_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `api_sync_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `badges`
--

DROP TABLE IF EXISTS `badges`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `badges` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `description` varchar(200) DEFAULT NULL,
  `criteria` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `badges`
--

LOCK TABLES `badges` WRITE;
/*!40000 ALTER TABLE `badges` DISABLE KEYS */;
/*!40000 ALTER TABLE `badges` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `batch`
--

DROP TABLE IF EXISTS `batch`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `batch` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `department_id` int DEFAULT NULL,
  `created_on` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_idx` (`department_id`),
  CONSTRAINT `department_id` FOREIGN KEY (`department_id`) REFERENCES `department` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `batch`
--

LOCK TABLES `batch` WRITE;
/*!40000 ALTER TABLE `batch` DISABLE KEYS */;
INSERT INTO `batch` VALUES (1,'2025',1,'2026-05-22 18:27:50'),(2,'2026',1,'2026-05-26 00:54:00');
/*!40000 ALTER TABLE `batch` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contest_stats`
--

DROP TABLE IF EXISTS `contest_stats`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `contest_stats` (
  `id` int NOT NULL AUTO_INCREMENT,
  `student_id` int DEFAULT NULL,
  `contest_rating` float DEFAULT '0',
  `contest_attended` int DEFAULT '0',
  `global_contest_rank` int DEFAULT NULL,
  `last_updated` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `stu_id_idx` (`student_id`),
  CONSTRAINT `stu_id` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contest_stats`
--

LOCK TABLES `contest_stats` WRITE;
/*!40000 ALTER TABLE `contest_stats` DISABLE KEYS */;
/*!40000 ALTER TABLE `contest_stats` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `department`
--

DROP TABLE IF EXISTS `department`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `department` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `university_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_idx` (`university_id`),
  CONSTRAINT `id` FOREIGN KEY (`university_id`) REFERENCES `university` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `department`
--

LOCK TABLES `department` WRITE;
/*!40000 ALTER TABLE `department` DISABLE KEYS */;
INSERT INTO `department` VALUES (1,'CS',1);
/*!40000 ALTER TABLE `department` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `enrollment_request`
--

DROP TABLE IF EXISTS `enrollment_request`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `enrollment_request` (
  `id` int NOT NULL AUTO_INCREMENT,
  `student_id` int DEFAULT NULL,
  `section_id` int DEFAULT NULL,
  `status` enum('PENDING','APPROVED','REJECTED') DEFAULT NULL,
  `requested_at` datetime DEFAULT NULL,
  `reviewed_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `student_id_idx` (`student_id`),
  KEY `section_id_idx` (`section_id`),
  CONSTRAINT `section` FOREIGN KEY (`section_id`) REFERENCES `section` (`id`),
  CONSTRAINT `student_id` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `enrollment_request`
--

LOCK TABLES `enrollment_request` WRITE;
/*!40000 ALTER TABLE `enrollment_request` DISABLE KEYS */;
INSERT INTO `enrollment_request` VALUES (1,2,1,'APPROVED','2026-05-26 01:00:51','2026-05-26 01:02:12'),(2,3,5,'APPROVED','2026-05-26 01:03:03','2026-05-26 01:26:50'),(3,4,1,'APPROVED','2026-05-26 01:23:50','2026-05-26 01:26:48'),(6,5,1,'REJECTED','2026-05-26 01:44:51','2026-05-26 01:45:07'),(7,5,1,'APPROVED','2026-05-26 01:45:38','2026-05-26 01:45:55'),(8,7,7,'APPROVED','2026-05-26 02:00:16','2026-05-26 02:00:35');
/*!40000 ALTER TABLE `enrollment_request` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `error_log`
--

DROP TABLE IF EXISTS `error_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `error_log` (
  `id` int NOT NULL AUTO_INCREMENT,
  `error_message` text,
  `stack_trace` text,
  `occured_at` datetime DEFAULT NULL,
  `source` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `error_log`
--

LOCK TABLES `error_log` WRITE;
/*!40000 ALTER TABLE `error_log` DISABLE KEYS */;
INSERT INTO `error_log` VALUES (1,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.Driver.GetResult(Int32 statementId, Int32 affectedRows, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.Driver.NextResult(Int32 statementId, Boolean force)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:31:17','StudentDL.AddStudent'),(2,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.Driver.GetResult(Int32 statementId, Int32 affectedRows, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.Driver.NextResult(Int32 statementId, Boolean force)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:31:17','StudentDL.AddStudent'),(3,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.Driver.NextResult(Int32 statementId, Boolean force)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:31:34','StudentDL.AddStudent'),(4,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:31:42','StudentDL.AddStudent'),(5,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:31:47','StudentDL.AddStudent'),(6,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:20','StudentDL.AddStudent'),(7,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:21','StudentDL.AddStudent'),(8,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:22','StudentDL.AddStudent'),(9,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:23','StudentDL.AddStudent'),(10,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:23','StudentDL.AddStudent'),(11,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:23','StudentDL.AddStudent'),(12,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:23','StudentDL.AddStudent'),(13,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:23','StudentDL.AddStudent'),(14,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:23','StudentDL.AddStudent'),(15,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:24','StudentDL.AddStudent'),(16,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:24','StudentDL.AddStudent'),(17,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:25','StudentDL.AddStudent'),(18,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:25','StudentDL.AddStudent'),(19,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:25','StudentDL.AddStudent'),(20,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:25','StudentDL.AddStudent'),(21,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:32:25','StudentDL.AddStudent'),(22,'Data too long for column \'Password_\' at row 1','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.StudentDL.AddStudent(Student student)','2026-05-26 00:36:02','StudentDL.AddStudent'),(23,'One or more errors occurred. (Response status code does not indicate success: 404 (Not Found).)','   at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)\r\n   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)\r\n   at UET_CODERANK.DL.LeetCodeAPI.GetSolvedStats(String username)','2026-05-26 01:03:43','LeetCodeAPI.GetSolvedStats'),(24,'One or more errors occurred. (Response status code does not indicate success: 404 (Not Found).)','   at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)\r\n   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)\r\n   at UET_CODERANK.DL.LeetCodeAPI.GetSolvedStats(String username)','2026-05-26 01:07:43','LeetCodeAPI.GetSolvedStats'),(25,'One or more errors occurred. (Response status code does not indicate success: 404 (Not Found).)','   at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)\r\n   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)\r\n   at UET_CODERANK.DL.LeetCodeAPI.GetSolvedStats(String username)','2026-05-26 01:17:02','LeetCodeAPI.GetSolvedStats'),(26,'One or more errors occurred. (Response status code does not indicate success: 404 (Not Found).)','   at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)\r\n   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)\r\n   at UET_CODERANK.DL.LeetCodeAPI.GetSolvedStats(String username)','2026-05-26 01:20:42','LeetCodeAPI.GetSolvedStats'),(27,'One or more errors occurred. (Response status code does not indicate success: 404 (Not Found).)','   at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)\r\n   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)\r\n   at UET_CODERANK.DL.LeetCodeAPI.GetSolvedStats(String username)','2026-05-26 01:23:33','LeetCodeAPI.GetSolvedStats'),(28,'One or more errors occurred. (Response status code does not indicate success: 404 (Not Found).)','   at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)\r\n   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)\r\n   at UET_CODERANK.DL.LeetCodeAPI.GetSolvedStats(String username)','2026-05-26 01:24:45','LeetCodeAPI.GetSolvedStats'),(29,'One or more errors occurred. (Response status code does not indicate success: 404 (Not Found).)','   at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)\r\n   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)\r\n   at UET_CODERANK.DL.LeetCodeAPI.GetSolvedStats(String username)','2026-05-26 01:24:49','LeetCodeAPI.GetSolvedStats'),(30,'One or more errors occurred. (Response status code does not indicate success: 404 (Not Found).)','   at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)\r\n   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)\r\n   at UET_CODERANK.DL.LeetCodeAPI.GetSolvedStats(String username)','2026-05-26 01:39:50','LeetCodeAPI.GetSolvedStats'),(31,'One or more errors occurred. (Response status code does not indicate success: 404 (Not Found).)','   at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)\r\n   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)\r\n   at UET_CODERANK.DL.LeetCodeAPI.GetSolvedStats(String username)','2026-05-26 01:44:02','LeetCodeAPI.GetSolvedStats'),(32,'One or more errors occurred. (Response status code does not indicate success: 404 (Not Found).)','   at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)\r\n   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)\r\n   at UET_CODERANK.DL.LeetCodeAPI.GetSolvedStats(String username)','2026-05-26 01:58:46','LeetCodeAPI.GetSolvedStats'),(33,'Cannot delete or update a parent row: a foreign key constraint fails (`db_final`.`section`, CONSTRAINT `batch_id` FOREIGN KEY (`batch_id`) REFERENCES `batch` (`id`))','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.Driver.GetResult(Int32 statementId, Int32 affectedRows, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.Driver.NextResult(Int32 statementId, Boolean force)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.BatchDL.DeleteBatch(Batch batch)','2026-05-28 12:52:22','BatchDL.DeleteBatch'),(34,'Cannot delete or update a parent row: a foreign key constraint fails (`db_final`.`section`, CONSTRAINT `batch_id` FOREIGN KEY (`batch_id`) REFERENCES `batch` (`id`))','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.Driver.GetResult(Int32 statementId, Int32 affectedRows, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.Driver.NextResult(Int32 statementId, Boolean force)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.BatchDL.DeleteBatch(Batch batch)','2026-05-28 12:52:24','BatchDL.DeleteBatch'),(35,'Cannot delete or update a parent row: a foreign key constraint fails (`db_final`.`enrollment_request`, CONSTRAINT `section` FOREIGN KEY (`section_id`) REFERENCES `section` (`id`))','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.SectionDL.DeleteSection(Section section)','2026-05-28 12:52:28','SectionDL.DeleteSection'),(36,'Cannot delete or update a parent row: a foreign key constraint fails (`db_final`.`enrollment_request`, CONSTRAINT `section` FOREIGN KEY (`section_id`) REFERENCES `section` (`id`))','   at MySql.Data.MySqlClient.MySqlStream.CheckForServerError()\r\n   at MySql.Data.MySqlClient.MySqlStream.ReadPacket()\r\n   at MySql.Data.MySqlClient.NativeDriver.GetResult(Int32 affectedRow, Int64 insertedId)\r\n   at MySql.Data.MySqlClient.MySqlDataReader.NextResult(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior, CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery(CancellationToken cancellationToken)\r\n   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\r\n   at UET_CODERANK.DL.DatabaseHelper.ExecuteNonQuery(String query, MySqlParameter[] parameters, CommandType commandType)\r\n   at UET_CODERANK.DL.SectionDL.DeleteSection(Section section)','2026-05-28 12:52:30','SectionDL.DeleteSection'),(37,'One or more errors occurred. (Response status code does not indicate success: 404 (Not Found).)','   at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)\r\n   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)\r\n   at UET_CODERANK.DL.LeetCodeAPI.GetSolvedStats(String username)','2026-05-28 12:52:49','LeetCodeAPI.GetSolvedStats');
/*!40000 ALTER TABLE `error_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `leaderboard_view`
--

DROP TABLE IF EXISTS `leaderboard_view`;
/*!50001 DROP VIEW IF EXISTS `leaderboard_view`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `leaderboard_view` AS SELECT 
 1 AS `student_id`,
 1 AS `name`,
 1 AS `reg_no`,
 1 AS `section_id`,
 1 AS `section_name`,
 1 AS `batch_id`,
 1 AS `batch_name`,
 1 AS `total_solved`,
 1 AS `easy_solved`,
 1 AS `medium_solved`,
 1 AS `hard_solved`,
 1 AS `global_ranking`,
 1 AS `contest_rating`,
 1 AS `contest_attended`,
 1 AS `score`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `leetcode_stats`
--

DROP TABLE IF EXISTS `leetcode_stats`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `leetcode_stats` (
  `id` int NOT NULL AUTO_INCREMENT,
  `student_id` int DEFAULT NULL,
  `total_solved` int DEFAULT '0',
  `easy_solved` int DEFAULT '0',
  `medium_solved` int DEFAULT '0',
  `hard_solved` int DEFAULT '0',
  `global_ranking` int DEFAULT NULL,
  `last_updated` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `student_id_UNIQUE` (`student_id`),
  CONSTRAINT `std_id` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `leetcode_stats`
--

LOCK TABLES `leetcode_stats` WRITE;
/*!40000 ALTER TABLE `leetcode_stats` DISABLE KEYS */;
INSERT INTO `leetcode_stats` VALUES (1,1,0,0,0,0,5000001,'2026-05-28 13:22:03'),(2,2,227,111,87,29,667174,'2026-05-26 01:28:09'),(3,3,253,60,141,52,579423,'2026-05-26 01:02:52'),(4,6,5,4,1,0,5000001,'2026-05-26 00:49:40'),(5,7,2157,560,1140,457,1518,'2026-05-26 02:23:56');
/*!40000 ALTER TABLE `leetcode_stats` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notification`
--

DROP TABLE IF EXISTS `notification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `notification` (
  `id` int NOT NULL AUTO_INCREMENT,
  `student_id` int DEFAULT NULL,
  `message` varchar(255) NOT NULL,
  `is_read` tinyint DEFAULT '0',
  `created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `ss_idx` (`student_id`),
  CONSTRAINT `ss` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notification`
--

LOCK TABLES `notification` WRITE;
/*!40000 ALTER TABLE `notification` DISABLE KEYS */;
/*!40000 ALTER TABLE `notification` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `pending_requests_view`
--

DROP TABLE IF EXISTS `pending_requests_view`;
/*!50001 DROP VIEW IF EXISTS `pending_requests_view`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `pending_requests_view` AS SELECT 
 1 AS `request_id`,
 1 AS `student_name`,
 1 AS `reg_no`,
 1 AS `email`,
 1 AS `leetcode_username`,
 1 AS `section_name`,
 1 AS `batch_name`,
 1 AS `status`,
 1 AS `requested_at`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `section`
--

DROP TABLE IF EXISTS `section`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `section` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(10) NOT NULL,
  `batch_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `batch_id_idx` (`batch_id`),
  CONSTRAINT `batch_id` FOREIGN KEY (`batch_id`) REFERENCES `batch` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `section`
--

LOCK TABLES `section` WRITE;
/*!40000 ALTER TABLE `section` DISABLE KEYS */;
INSERT INTO `section` VALUES (1,'A',1),(2,'A',2),(4,'B',2),(5,'B',1),(7,'D',1),(9,'C',2);
/*!40000 ALTER TABLE `section` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student`
--

DROP TABLE IF EXISTS `student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student` (
  `id` int NOT NULL AUTO_INCREMENT,
  `reg_no` varchar(20) NOT NULL,
  `name` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(255) NOT NULL,
  `leetcode_username` varchar(100) DEFAULT NULL,
  `profile_pic_path` varchar(255) DEFAULT NULL,
  `profile_name` varchar(75) DEFAULT NULL,
  `is_approved` tinyint DEFAULT '0',
  `created_at` datetime DEFAULT NULL,
  `section_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `reg_no_UNIQUE` (`reg_no`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  UNIQUE KEY `leetcode_username_UNIQUE` (`leetcode_username`),
  KEY `section_idx` (`section_id`),
  KEY `section_student_idx` (`section_id`),
  CONSTRAINT `section_student` FOREIGN KEY (`section_id`) REFERENCES `section` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student`
--

LOCK TABLES `student` WRITE;
/*!40000 ALTER TABLE `student` DISABLE KEYS */;
INSERT INTO `student` VALUES (1,'2025-CS-00','Test','test@gmail.com','$2a$11$a//HgsgL2OMTW5N3Y9jhp.zw/30cRQzxUhblvfZyJaNpYkdcmUPkW','mUmarSaga','https://assets.leetcode.com/users/avatars/avatar_1688985344.png','whimsiren',1,'2026-05-21 19:55:18',1),(2,'2025-CS-01','Mirru','Miruu@gmail.com','$2a$11$BpbJDyZlLEvY3kbOdl1n0eHgk4J4c/DWlMAmHgBwoZW/WAKx5JKHu','fjzzq2002','https://assets.leetcode.com/users/fjzzq2002/avatar_1735951462.png','Miruu',1,'2026-05-26 00:37:24',1),(3,'2025-CS-02','Neal Wu','NealWu@gmail.com','$2a$11$QXhFsML6hoOjdkaX67FLv.H1R6hF/RiAUW.V6wpvidW3qfnUdHBLy','neal_wu','https://assets.leetcode.com/users/neal_wu/avatar_1737814509.png','Neal Wu',1,'2026-05-26 00:43:36',5),(4,'2025-CS-03','Yawn_Sean','Yawn_Sean@gmail.com','$2a$11$jOHap7wLLDWATrY8jAhNDuRaF/g0fbjyBaEm0Kvny7LglSjgAfQO.',NULL,NULL,NULL,1,'2026-05-26 00:44:08',1),(5,'2025-CS-04','lol','lol@gmail.com','$2a$11$72J19pMJowYDGWoCHBdIsusAM1vOT1gcwfYw4jZmgFD.dooP2V2c6','yawn_sean','https://assets.leetcode.com/users/1900015431/avatar_1633874346.png','Yawn_Sean',1,'2026-05-26 00:45:14',1),(6,'2025-CS-05','heltion','heltion@gmail.com','$2a$11$.WUGp.CX2cF/xlZQYRiu9e4AIdp/LLzArw3dWZQlWdGT51ADgufnq','heltion','https://assets.leetcode.com/users/default_avatar.jpg','Heltion',0,'2026-05-26 00:49:38',NULL),(7,'2025-CS-08','Joshua Chen','JoshuaChen@gmail.com','$2a$11$3E3jMcJHS9z3eO/YcyjOxO.9snMcpqH8mnW1dKG75ztvA8zcm7E/u','numb3r5','https://assets.leetcode.com/users/default_avatar.jpg','Joshua Chen',1,'2026-05-26 00:51:13',7);
/*!40000 ALTER TABLE `student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student_badges`
--

DROP TABLE IF EXISTS `student_badges`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student_badges` (
  `id` int NOT NULL AUTO_INCREMENT,
  `student_id` int DEFAULT NULL,
  `badge_id` int DEFAULT NULL,
  `awarded_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `d_idx` (`student_id`),
  KEY `badge_idx` (`badge_id`),
  CONSTRAINT `badge` FOREIGN KEY (`badge_id`) REFERENCES `badges` (`id`),
  CONSTRAINT `d` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student_badges`
--

LOCK TABLES `student_badges` WRITE;
/*!40000 ALTER TABLE `student_badges` DISABLE KEYS */;
/*!40000 ALTER TABLE `student_badges` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `student_section_view`
--

DROP TABLE IF EXISTS `student_section_view`;
/*!50001 DROP VIEW IF EXISTS `student_section_view`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `student_section_view` AS SELECT 
 1 AS `student_id`,
 1 AS `name`,
 1 AS `reg_no`,
 1 AS `email`,
 1 AS `leetcode_username`,
 1 AS `is_approved`,
 1 AS `section_id`,
 1 AS `section_name`,
 1 AS `batch_id`,
 1 AS `batch_name`,
 1 AS `department_name`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `student_stats_view`
--

DROP TABLE IF EXISTS `student_stats_view`;
/*!50001 DROP VIEW IF EXISTS `student_stats_view`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `student_stats_view` AS SELECT 
 1 AS `student_id`,
 1 AS `name`,
 1 AS `reg_no`,
 1 AS `leetcode_username`,
 1 AS `total_solved`,
 1 AS `easy_solved`,
 1 AS `medium_solved`,
 1 AS `hard_solved`,
 1 AS `global_ranking`,
 1 AS `contest_rating`,
 1 AS `contest_attended`,
 1 AS `score`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `top_performers_view`
--

DROP TABLE IF EXISTS `top_performers_view`;
/*!50001 DROP VIEW IF EXISTS `top_performers_view`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `top_performers_view` AS SELECT 
 1 AS `student_id`,
 1 AS `name`,
 1 AS `reg_no`,
 1 AS `section_name`,
 1 AS `batch_name`,
 1 AS `total_solved`,
 1 AS `hard_solved`,
 1 AS `score`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `university`
--

DROP TABLE IF EXISTS `university`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `university` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `university`
--

LOCK TABLES `university` WRITE;
/*!40000 ALTER TABLE `university` DISABLE KEYS */;
INSERT INTO `university` VALUES (1,'UET Lahore');
/*!40000 ALTER TABLE `university` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `weekly_snapshot`
--

DROP TABLE IF EXISTS `weekly_snapshot`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `weekly_snapshot` (
  `id` int NOT NULL AUTO_INCREMENT,
  `student_id` int DEFAULT NULL,
  `week_start` date DEFAULT NULL,
  `total_solved` int DEFAULT NULL,
  `easy_solved` int DEFAULT NULL,
  `medium_solved` int DEFAULT NULL,
  `hard_solved` int DEFAULT NULL,
  `recorded_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `_id_idx` (`student_id`),
  CONSTRAINT `_id` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `weekly_snapshot`
--

LOCK TABLES `weekly_snapshot` WRITE;
/*!40000 ALTER TABLE `weekly_snapshot` DISABLE KEYS */;
INSERT INTO `weekly_snapshot` VALUES (1,1,'2026-03-31',280,130,110,40,'2026-05-28 13:07:57'),(2,1,'2026-04-07',295,137,116,42,'2026-05-28 13:07:57'),(3,1,'2026-04-14',305,141,120,44,'2026-05-28 13:07:57'),(4,1,'2026-04-21',312,145,123,44,'2026-05-28 13:07:57'),(5,1,'2026-04-28',320,148,126,46,'2026-05-28 13:07:57'),(6,1,'2026-05-05',335,152,130,53,'2026-05-28 13:07:57');
/*!40000 ALTER TABLE `weekly_snapshot` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Final view structure for view `leaderboard_view`
--

/*!50001 DROP VIEW IF EXISTS `leaderboard_view`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `leaderboard_view` AS select `s`.`id` AS `student_id`,`s`.`name` AS `name`,`s`.`reg_no` AS `reg_no`,`sec`.`id` AS `section_id`,`sec`.`name` AS `section_name`,`b`.`id` AS `batch_id`,`b`.`name` AS `batch_name`,coalesce(`ls`.`total_solved`,0) AS `total_solved`,coalesce(`ls`.`easy_solved`,0) AS `easy_solved`,coalesce(`ls`.`medium_solved`,0) AS `medium_solved`,coalesce(`ls`.`hard_solved`,0) AS `hard_solved`,coalesce(`ls`.`global_ranking`,0) AS `global_ranking`,coalesce(`cs`.`contest_rating`,0) AS `contest_rating`,coalesce(`cs`.`contest_attended`,0) AS `contest_attended`,(((coalesce(`ls`.`easy_solved`,0) * 1) + (coalesce(`ls`.`medium_solved`,0) * 3)) + (coalesce(`ls`.`hard_solved`,0) * 5)) AS `score` from ((((`student` `s` join `section` `sec` on((`s`.`section_id` = `sec`.`id`))) join `batch` `b` on((`sec`.`batch_id` = `b`.`id`))) left join `leetcode_stats` `ls` on((`s`.`id` = `ls`.`student_id`))) left join `contest_stats` `cs` on((`s`.`id` = `cs`.`student_id`))) where (`s`.`is_approved` = 1) order by (((coalesce(`ls`.`easy_solved`,0) * 1) + (coalesce(`ls`.`medium_solved`,0) * 3)) + (coalesce(`ls`.`hard_solved`,0) * 5)) desc */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `pending_requests_view`
--

/*!50001 DROP VIEW IF EXISTS `pending_requests_view`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `pending_requests_view` AS select `er`.`id` AS `request_id`,`s`.`name` AS `student_name`,`s`.`reg_no` AS `reg_no`,`s`.`email` AS `email`,`s`.`leetcode_username` AS `leetcode_username`,`sec`.`name` AS `section_name`,`b`.`name` AS `batch_name`,`er`.`status` AS `status`,`er`.`requested_at` AS `requested_at` from (((`enrollment_request` `er` join `student` `s` on((`er`.`student_id` = `s`.`id`))) join `section` `sec` on((`er`.`section_id` = `sec`.`id`))) join `batch` `b` on((`sec`.`batch_id` = `b`.`id`))) where (`er`.`status` = 'PENDING') */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `student_section_view`
--

/*!50001 DROP VIEW IF EXISTS `student_section_view`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `student_section_view` AS select `s`.`id` AS `student_id`,`s`.`name` AS `name`,`s`.`reg_no` AS `reg_no`,`s`.`email` AS `email`,`s`.`leetcode_username` AS `leetcode_username`,`s`.`is_approved` AS `is_approved`,`sec`.`id` AS `section_id`,`sec`.`name` AS `section_name`,`b`.`id` AS `batch_id`,`b`.`name` AS `batch_name`,`d`.`name` AS `department_name` from (((`student` `s` left join `section` `sec` on((`s`.`section_id` = `sec`.`id`))) left join `batch` `b` on((`sec`.`batch_id` = `b`.`id`))) left join `department` `d` on((`b`.`department_id` = `d`.`id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `student_stats_view`
--

/*!50001 DROP VIEW IF EXISTS `student_stats_view`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `student_stats_view` AS select `s`.`id` AS `student_id`,`s`.`name` AS `name`,`s`.`reg_no` AS `reg_no`,`s`.`leetcode_username` AS `leetcode_username`,coalesce(`ls`.`total_solved`,0) AS `total_solved`,coalesce(`ls`.`easy_solved`,0) AS `easy_solved`,coalesce(`ls`.`medium_solved`,0) AS `medium_solved`,coalesce(`ls`.`hard_solved`,0) AS `hard_solved`,coalesce(`ls`.`global_ranking`,0) AS `global_ranking`,coalesce(`cs`.`contest_rating`,0) AS `contest_rating`,coalesce(`cs`.`contest_attended`,0) AS `contest_attended`,(((coalesce(`ls`.`easy_solved`,0) * 1) + (coalesce(`ls`.`medium_solved`,0) * 3)) + (coalesce(`ls`.`hard_solved`,0) * 5)) AS `score` from ((`student` `s` left join `leetcode_stats` `ls` on((`s`.`id` = `ls`.`student_id`))) left join `contest_stats` `cs` on((`s`.`id` = `cs`.`student_id`))) where (`s`.`is_approved` = 1) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `top_performers_view`
--

/*!50001 DROP VIEW IF EXISTS `top_performers_view`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `top_performers_view` AS select `s`.`id` AS `student_id`,`s`.`name` AS `name`,`s`.`reg_no` AS `reg_no`,`sec`.`name` AS `section_name`,`b`.`name` AS `batch_name`,coalesce(`ls`.`total_solved`,0) AS `total_solved`,coalesce(`ls`.`hard_solved`,0) AS `hard_solved`,(((coalesce(`ls`.`easy_solved`,0) * 1) + (coalesce(`ls`.`medium_solved`,0) * 3)) + (coalesce(`ls`.`hard_solved`,0) * 5)) AS `score` from (((`student` `s` left join `leetcode_stats` `ls` on((`s`.`id` = `ls`.`student_id`))) left join `section` `sec` on((`s`.`section_id` = `sec`.`id`))) left join `batch` `b` on((`sec`.`batch_id` = `b`.`id`))) where (`s`.`is_approved` = 1) order by `score` desc limit 10 */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-05-28 14:11:09
