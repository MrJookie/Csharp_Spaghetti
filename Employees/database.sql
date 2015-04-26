SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `csharp_accounts`
-- ----------------------------
DROP TABLE IF EXISTS `csharp_accounts`;
CREATE TABLE `csharp_accounts` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(10) NOT NULL,
  `user_pass` varchar(40) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of csharp_accounts
-- ----------------------------
INSERT INTO `csharp_accounts` VALUES ('1', 'admin', 'd033e22ae348aeb5660fc2140aec35850c4da997');

-- ----------------------------
-- Table structure for `csharp_employees`
-- ----------------------------
DROP TABLE IF EXISTS `csharp_employees`;
CREATE TABLE `csharp_employees` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) NOT NULL,
  `surname` varchar(20) NOT NULL,
  `birth_date` varchar(10) NOT NULL,
  `gender` varchar(6) NOT NULL,
  `country` varchar(20) NOT NULL,
  `address` varchar(50) NOT NULL,
  `telephone` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of csharp_employees
-- ----------------------------
INSERT INTO `csharp_employees` VALUES ('1', 'Jan', 'Novák', '1992-02-08', 'Male', 'CZ', 'Pražská', '776123411');
INSERT INTO `csharp_employees` VALUES ('2', 'Jakub', 'Novák', '1980-12-21', 'Male', 'CZ', 'Liberecká', '723942311');
INSERT INTO `csharp_employees` VALUES ('3', 'Ondrej', 'Novotný', '1989-11-15', 'Male', 'CZ', 'Brnenská', '605422374');
INSERT INTO `csharp_employees` VALUES ('4', 'Zdenek', 'Dvorák', '1972-07-01', 'Male', 'SK', 'Košická', '623176281');
