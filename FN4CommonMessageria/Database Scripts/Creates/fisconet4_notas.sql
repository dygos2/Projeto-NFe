delimiter $$

CREATE TABLE `notas` (
  `NFe_ide_nNF` int(9) unsigned NOT NULL DEFAULT '0',
  `NFe_emit_CNPJ` varchar(15) NOT NULL DEFAULT ' ',
  `serie` tinyint(3) NOT NULL DEFAULT '0',
  `pastaDeTrabalho` varchar(100) DEFAULT NULL,
  `statusDaNota` tinyint(4) DEFAULT NULL,
  `tentativasDeInclusao` int(10) unsigned NOT NULL DEFAULT '0',
  `NFe_ide_dEmi` date DEFAULT NULL,
  `NFe_infNFe_id` varchar(50) DEFAULT NULL,
  `NFe_emit_CPF` varchar(11) DEFAULT NULL,
  `NFe_emit_xNome` varchar(60) DEFAULT NULL,
  `NFe_dest_CNPJ` varchar(15) DEFAULT NULL,
  `NFe_dest_CPF` varchar(11) DEFAULT NULL,
  `NFe_dest_xNome` varchar(60) DEFAULT NULL,
  `NFe_dest_UF` varchar(2) DEFAULT NULL,
  `NFe_total_ICMSTot_vNF` decimal(15,2) DEFAULT NULL,
  `retEnviNFe_infRec_nRec` varchar(15) DEFAULT NULL,
  `retEnviNFe_cStat` varchar(3) DEFAULT NULL,
  `retEnviNFe_xMotivo` varchar(255) DEFAULT NULL,
  `protNfe_nProt` varchar(15) DEFAULT NULL,
  `dataUltimaAtualizacao` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `impressaoSolicitada` tinyint(1) DEFAULT '0',
  `impressoEmContingencia` tinyint(1) DEFAULT '0',
  `impressora` varchar(100) DEFAULT NULL,
  `sincronizada` tinyint(1) DEFAULT '0',
  `imprimeDanfe` bit(1) NOT NULL DEFAULT b'0',
  `emailDest` varchar(60) DEFAULT NULL,
  `ret_post_data` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`NFe_ide_nNF`,`NFe_emit_CNPJ`,`serie`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT$$

