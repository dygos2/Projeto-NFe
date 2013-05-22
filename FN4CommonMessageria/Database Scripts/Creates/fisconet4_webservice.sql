delimiter $$

CREATE TABLE `webservice` (
  `idWebservice` int(11) NOT NULL AUTO_INCREMENT,
  `uf` varchar(4) DEFAULT NULL,
  `nome` varchar(60) DEFAULT NULL,
  `versao` varchar(4) DEFAULT NULL,
  `url` varchar(128) DEFAULT NULL,
  `homologacao` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`idWebservice`),
  UNIQUE KEY `idwebservice_UNIQUE` (`idWebservice`),
  UNIQUE KEY `URL_UNIQUE` (`url`)
) ENGINE=InnoDB AUTO_INCREMENT=178 DEFAULT CHARSET=utf8$$

