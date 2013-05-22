delimiter $$

CREATE TABLE `historico` (
  `idHistorico` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `NFe_ide_nNF` int(10) unsigned NOT NULL DEFAULT '0',
  `NFe_emit_CNPJ` varchar(15) NOT NULL,
  `dataHora` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `complemento` text,
  `idTpHistorico` int(10) unsigned NOT NULL,
  PRIMARY KEY (`idHistorico`),
  KEY `NFe_ide_nNF` (`NFe_ide_nNF`,`NFe_emit_CNPJ`),
  KEY `idTpHistorico` (`idTpHistorico`),
  CONSTRAINT `historico_ibfk_1` FOREIGN KEY (`NFe_ide_nNF`, `NFe_emit_CNPJ`) REFERENCES `notas` (`NFe_ide_nNF`, `NFe_emit_CNPJ`),
  CONSTRAINT `historico_ibfk_2` FOREIGN KEY (`idTpHistorico`) REFERENCES `tipodehistorico` (`idTpHistorico`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT$$

