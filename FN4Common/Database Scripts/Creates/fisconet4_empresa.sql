delimiter $$

CREATE TABLE `empresa` (
  `idEmpresa` int(11) NOT NULL AUTO_INCREMENT,
  `cnpj` varchar(14) DEFAULT NULL,
  `cpf` varchar(11) DEFAULT NULL,
  `nome` varchar(60) NOT NULL,
  `nomeFantasia` varchar(60) DEFAULT NULL,
  `logradouro` varchar(60) NOT NULL,
  `numero` varchar(60) NOT NULL,
  `complemento` varchar(60) DEFAULT NULL,
  `bairro` varchar(60) NOT NULL,
  `codigoMunicipio` int(7) NOT NULL,
  `nomeMunicipio` varchar(60) NOT NULL,
  `uf` varchar(2) NOT NULL,
  `cep` varchar(8) DEFAULT NULL,
  `codigoPais` int(4) DEFAULT NULL,
  `nomePais` varchar(60) DEFAULT NULL,
  `fone` varchar(14) DEFAULT NULL,
  `ie` varchar(14) NOT NULL,
  `iest` varchar(14) DEFAULT NULL,
  `im` varchar(15) DEFAULT NULL,
  `cnae` varchar(7) DEFAULT NULL,
  `crt` int(1) NOT NULL,
  `homologacao` int(1) NOT NULL DEFAULT 0,
  `receberEmailNota` bit(1) DEFAULT b'0',
  `email` varchar(60) DEFAULT NULL,
  `token` varchar(60) NOT NULL DEFAULT '0',
  PRIMARY KEY (`idEmpresa`),
  UNIQUE KEY `idEmpresa_UNIQUE` (`idEmpresa`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8$$

