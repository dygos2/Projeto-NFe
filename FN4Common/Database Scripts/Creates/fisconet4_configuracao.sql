delimiter $$

CREATE TABLE `configuracao` (
  `idConfiguracao` int(11) NOT NULL AUTO_INCREMENT,
  `chave` varchar(20) NOT NULL,
  `valor` varchar(255) DEFAULT NULL,
  `idEmpresa` int(11) NOT NULL,
  PRIMARY KEY (`idConfiguracao`),
  KEY `FK_idEmpresa` (`idEmpresa`),
  CONSTRAINT `FK_idEmpresa` FOREIGN KEY (`idEmpresa`) REFERENCES `empresa` (`idEmpresa`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8$$

