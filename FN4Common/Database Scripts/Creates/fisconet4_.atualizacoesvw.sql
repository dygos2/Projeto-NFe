delimiter $$

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `fisconet4_homologacao20`.`atualizacoesvw` AS select `n`.`NFe_ide_nNF` AS `NFe_ide_nNF`,`n`.`NFe_emit_CNPJ` AS `NFe_emit_cnpj`,`n`.`NFe_dest_xNome` AS `NFe_dest_xNome`,`h`.`dataHora` AS `dataHora`,concat_ws(_utf8' - ',`t`.`tpHistorico`,`h`.`complemento`) AS `atualizacao`,`h`.`idHistorico` AS `idHistorico` from (`fisconet4_homologacao20`.`notas` `n` join (`fisconet4_homologacao20`.`historico` `h` left join `fisconet4_homologacao20`.`tipodehistorico` `t` on((`h`.`idTpHistorico` = `t`.`idTpHistorico`)))) where ((`h`.`NFe_ide_nNF` = `n`.`NFe_ide_nNF`) and (`h`.`NFe_emit_CNPJ` = `n`.`NFe_emit_CNPJ`)) order by `h`.`idHistorico` desc$$

