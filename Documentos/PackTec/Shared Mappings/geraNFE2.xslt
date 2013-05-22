<xsl:stylesheet xmlns="http://www.portalfiscal.inf.br/nfe" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output encoding="UTF-8" indent="yes" method="xml"></xsl:output>
	<xsl:template match="/">
		<NFe>
			<infNFe versao="1.10">
				<xsl:attribute name="Id">
					<xsl:value-of select="NFe/infNFe/@Id"></xsl:value-of>
				</xsl:attribute>
				<ide>
					<cUF>
						<xsl:value-of select="NFe/infNFe/ide/cUF"></xsl:value-of>
					</cUF>
					<cNF>
						<xsl:value-of select="format-number(NFe/infNFe/ide/cNF,'000000000')"></xsl:value-of>
					</cNF>
					<natOp>
						<xsl:value-of select="NFe/infNFe/ide/natOp"></xsl:value-of>
					</natOp>
					<indPag>
						<xsl:value-of select="NFe/infNFe/ide/indPag"></xsl:value-of>
					</indPag>
					<mod>
						<xsl:value-of select="NFe/infNFe/ide/mod"></xsl:value-of>
					</mod>
					<serie>
						<xsl:value-of select="number(NFe/infNFe/ide/serie)"></xsl:value-of>
					</serie>
					<nNF>
						<xsl:value-of select="number(NFe/infNFe/ide/nNF)"></xsl:value-of>
					</nNF>
					<dEmi>
						<xsl:value-of select="NFe/infNFe/ide/dEmi"></xsl:value-of>
					</dEmi>
					<xsl:if test="NFe/infNFe/ide/dSaiEnt != '' ">
						<dSaiEnt>
							<xsl:value-of select="NFe/infNFe/ide/dSaiEnt"></xsl:value-of>
						</dSaiEnt>
					</xsl:if>
					<tpNF>
						<xsl:value-of select="number(NFe/infNFe/ide/tpNF)"></xsl:value-of>
					</tpNF>
					<cMunFG>
						<xsl:value-of select="NFe/infNFe/ide/cMunFG"></xsl:value-of>
					</cMunFG>
					<tpImp>
						<xsl:value-of select="number(NFe/infNFe/ide/tpImp)"></xsl:value-of>
					</tpImp>
					<tpEmis>
						<xsl:value-of select="NFe/infNFe/ide/tpEmis"></xsl:value-of>
					</tpEmis>
					<cDV>
						<xsl:value-of select="NFe/infNFe/ide/cDV"></xsl:value-of>
					</cDV>
					<tpAmb>
						<xsl:value-of select="NFe/infNFe/ide/tpAmb"></xsl:value-of>
					</tpAmb>
					<finNFe>1</finNFe>
					<procEmi>0</procEmi>
					<verProc>1.10</verProc>
				</ide>
				<emit>
					<CNPJ>
						<xsl:value-of select="NFe/infNFe/emit/CNPJ"></xsl:value-of>
					</CNPJ>
					<xNome>
						<xsl:value-of select="NFe/infNFe/emit/xNome"></xsl:value-of>
					</xNome>
					<xFant>
						<xsl:value-of select="NFe/infNFe/emit/xFant"></xsl:value-of>
					</xFant>
					<enderEmit>
						<xLgr>
							<xsl:value-of select="NFe/infNFe/emit/enderEmit/xLgr"></xsl:value-of>
						</xLgr>
						<nro>
							<xsl:value-of select="NFe/infNFe/emit/enderEmit/nro"></xsl:value-of>
						</nro>
						<xsl:if test="NFe/infNFe/emit/xCpl != '' ">
							<xCpl>
								<xsl:value-of select="NFe/infNFe/emit/enderEmit/xCpl"></xsl:value-of>
							</xCpl>
						</xsl:if>
						<xsl:if test="NFe/infNFe/emit/enderEmit/xBairro != ''">
							<xBairro>
								<xsl:value-of select="NFe/infNFe/emit/enderEmit/xBairro"></xsl:value-of>
							</xBairro>
						</xsl:if>
						<xsl:if test="NFe/infNFe/emit/enderEmit/xBairro = ''">
							<xBairro>-</xBairro>
						</xsl:if>
						<cMun>
							<xsl:value-of select="NFe/infNFe/emit/enderEmit/cMun"></xsl:value-of>
						</cMun>
						<xMun>
							<xsl:value-of select="NFe/infNFe/emit/enderEmit/xMun"></xsl:value-of>
						</xMun>
						<UF>
							<xsl:value-of select="NFe/infNFe/emit/enderEmit/UF"></xsl:value-of>
						</UF>
						<CEP>
							<xsl:value-of select="NFe/infNFe/emit/enderEmit/CEP"></xsl:value-of>
						</CEP>
						<cPais>
							<xsl:value-of select="NFe/infNFe/emit/enderEmit/cPais"></xsl:value-of>
						</cPais>
						<xPais>
							<xsl:value-of select="NFe/infNFe/emit/enderEmit/xPais"></xsl:value-of>
						</xPais>
						<xsl:if test="NFe/infNFe/emit/enderEmit/fone != ''">
							<fone>
								<xsl:value-of select="number(NFe/infNFe/emit/enderEmit/fone)"></xsl:value-of>
							</fone>
						</xsl:if>
					</enderEmit>

					<IE>
						<xsl:value-of select="NFe/infNFe/emit/IE"></xsl:value-of>
					</IE>

					<xsl:if test="NFe/infNFe/emit/IM != ''">
						<IM>
							<xsl:value-of select="NFe/infNFe/emit/IM"></xsl:value-of>
						</IM>
					</xsl:if>
				</emit>
				<dest>
					<xsl:if test="NFe/infNFe/dest/CNPJ != ''">
						<CNPJ>
							<xsl:value-of select="NFe/infNFe/dest/CNPJ"></xsl:value-of>
						</CNPJ>
					</xsl:if>
					<xsl:if test="NFe/infNFe/dest/CPF != '' and  NFe/infNFe/dest/CPF != 0">
						<CPF>
							<xsl:value-of select="translate(NFe/infNFe/dest/CPF,'-','')"></xsl:value-of>
						</CPF>
					</xsl:if>
					<xsl:if test="NFe/infNFe/dest/CPF = '' and  NFe/infNFe/dest/CNPJ = '' ">
						<CNPJ></CNPJ>
					</xsl:if>
					<xNome>
						<xsl:value-of select="NFe/infNFe/dest/xNome"></xsl:value-of>
					</xNome>
					<enderDest>
						<xLgr>
							<xsl:value-of select="NFe/infNFe/dest/enderDest/xLgr"></xsl:value-of>
						</xLgr>
						<nro>
							<xsl:value-of select="NFe/infNFe/dest/enderDest/nro"></xsl:value-of>
						</nro>
						<xsl:if test="NFe/infNFe/dest/enderDest/xCpl != ''">
							<xCpl>
								<xsl:value-of select="NFe/infNFe/dest/enderDest/xCpl"></xsl:value-of>
							</xCpl>
						</xsl:if>
						<xsl:if test="NFe/infNFe/dest/enderDest/xBairro != ''">
							<xBairro>
								<xsl:value-of select="NFe/infNFe/dest/enderDest/xBairro"></xsl:value-of>
							</xBairro>
						</xsl:if>
						<xsl:if test="NFe/infNFe/dest/enderDest/xBairro = '' ">
							<xBairro>-</xBairro>
						</xsl:if>
						<cMun>
							<xsl:value-of select="NFe/infNFe/dest/enderDest/cMun"></xsl:value-of>
						</cMun>
						<xMun>
							<xsl:value-of select="NFe/infNFe/dest/enderDest/xMun"></xsl:value-of>
						</xMun>
						<UF>
							<xsl:value-of select="NFe/infNFe/dest/enderDest/UF"></xsl:value-of>
						</UF>
						<xsl:if test="NFe/infNFe/dest/enderDest/CEP != '' and NFe/infNFe/dest/enderDest/CEP != 0 ">
							<CEP>
								<xsl:value-of select="NFe/infNFe/dest/enderDest/CEP"></xsl:value-of>
							</CEP>
						</xsl:if>
						<cPais>
							<xsl:value-of select="NFe/infNFe/dest/enderDest/cPais"></xsl:value-of>
						</cPais>
						<xPais>
							<xsl:value-of select="NFe/infNFe/dest/enderDest/xPais"></xsl:value-of>
						</xPais>
						<xsl:if test="NFe/infNFe/dest/enderDest/fone != ''">
							<fone>
								<xsl:value-of select="NFe/infNFe/dest/enderDest/fone"></xsl:value-of>
							</fone>
						</xsl:if>
					</enderDest>
					<IE>
						<xsl:value-of select="NFe/infNFe/dest/IE"></xsl:value-of>
					</IE>
					<xsl:if test="/NFe/infNFe/dest/ISUF != '' and /NFe/infNFe/dest/ISUF != 0 ">
						<ISUF>
							<xsl:value-of select="NFe/infNFe/dest/ISUF"></xsl:value-of>
						</ISUF>
					</xsl:if>
				</dest>
				<xsl:if test="NFe/infNFe/retirada/CNPJ != ''">
					<retirada>
						<CNPJ>
							<xsl:value-of select="NFe/infNFe/retirada/CNPJ"></xsl:value-of>
						</CNPJ>
						<xLgr>
							<xsl:value-of select="NFe/infNFe/retirada/xLgr"></xsl:value-of>
						</xLgr>
						<nro>
							<xsl:value-of select="NFe/infNFe/retirada/nro"></xsl:value-of>
						</nro>
						<xsl:if test="NFe/infNFe/retirada/xCpl != ''">
							<xCpl>
								<xsl:value-of select="NFe/infNFe/retirada/xCpl"></xsl:value-of>
							</xCpl>
						</xsl:if>
						<xsl:if test="NFe/infNFe/retirada/xBairro != ''">
							<xBairro>
								<xsl:value-of select="NFe/infNFe/retirada/xBairro"></xsl:value-of>
							</xBairro>
						</xsl:if>
						<xsl:if test="NFe/infNFe/retirada/xBairro = ''">
							<xBairro>-</xBairro>
						</xsl:if>
						<cMun>
							<xsl:value-of select="NFe/infNFe/retirada/cMun"></xsl:value-of>
						</cMun>
						<xMun>
							<xsl:value-of select="NFe/infNFe/retirada/xMun"></xsl:value-of>
						</xMun>
						<UF>
							<xsl:value-of select="NFe/infNFe/retirada/UF"></xsl:value-of>
						</UF>
					</retirada>
				</xsl:if>
				<xsl:if test="NFe/infNFe/entrega/CNPJ != ''">
					<entrega>
						<CNPJ>
							<xsl:value-of select="NFe/infNFe/entrega/CNPJ"></xsl:value-of>
						</CNPJ>
						<xLgr>
							<xsl:value-of select="NFe/infNFe/entrega/xLgr"></xsl:value-of>
						</xLgr>
						<nro>
							<xsl:value-of select="NFe/infNFe/entrega/nro"></xsl:value-of>
						</nro>
						<xsl:if test="NFe/infNFe/entrega/xCpl != ''">
							<xCpl>
								<xsl:value-of select="NFe/infNFe/entrega/xCpl"></xsl:value-of>
							</xCpl>
						</xsl:if>
						<xsl:if test="NFe/infNFe/entrega/xBairro != ''">
							<xBairro>
								<xsl:value-of select="NFe/infNFe/entrega/xBairro"></xsl:value-of>
							</xBairro>
						</xsl:if>
						<xsl:if test="NFe/infNFe/entrega/xBairro = ''">
							<xBairro>-</xBairro>
						</xsl:if>
						<cMun>
							<xsl:value-of select="NFe/infNFe/entrega/cMun"></xsl:value-of>
						</cMun>
						<xMun>
							<xsl:value-of select="NFe/infNFe/entrega/xMun"></xsl:value-of>
						</xMun>
						<UF>
							<xsl:value-of select="NFe/infNFe/entrega/UF"></xsl:value-of>
						</UF>
					</entrega>
				</xsl:if>




				<xsl:for-each select="NFe/infNFe/det">
					<det>
						<xsl:attribute name="nItem">
							<xsl:value-of select="@nItem"></xsl:value-of>
						</xsl:attribute>


						<prod>
							<cProd>
								<xsl:value-of select="prod/cProd"></xsl:value-of>
							</cProd>
							<!-- inserido por obrigatoriedade -->
							<cEAN></cEAN>
							<xProd>
								<xsl:value-of select="prod/xProd"></xsl:value-of>
							</xProd>
							<xsl:if test="prod/NCM != ''">
								<NCM>
									<xsl:value-of select="prod/NCM"></xsl:value-of>
								</NCM>
							</xsl:if>
							<xsl:if test="prod/genero != ''">
								<genero>
									<xsl:value-of select="prod/genero"></xsl:value-of>
								</genero>
							</xsl:if>
							<xsl:if test="prod/genero = ''">
								<genero>0</genero>
							</xsl:if>
							<CFOP>
								<xsl:value-of select="prod/CFOP"></xsl:value-of>
							</CFOP>
							<uCom>
								<xsl:value-of select="prod/uCom"></xsl:value-of>
							</uCom>
							<qCom>
								<xsl:value-of select="translate(format-number(prod/qCom,'#0.0000'),',','.')"></xsl:value-of>
							</qCom>
							<xsl:if test="not(prod/vUnCom)">
								<vUnCom>
									<xsl:value-of select="translate(format-number(prod/vProd div prod/qCom,'#0.0000'),',','.')"></xsl:value-of>
								</vUnCom>
							</xsl:if>
							<xsl:if test="prod/vUnCom">
								<vUnCom>
									<xsl:value-of select="format-number(prod/vUnCom,'#0.0000')"></xsl:value-of>
								</vUnCom>
							</xsl:if>
							<vProd>
								<xsl:value-of select="format-number(number(prod/vProd),'#0.00')"></xsl:value-of>
							</vProd>
							<!-- inserido por obrigatoriedade -->
							<cEANTrib></cEANTrib>
							<xsl:if test="prod/uTrib != ''">
								<uTrib>
									<xsl:value-of select="prod/uTrib"></xsl:value-of>
								</uTrib>
							</xsl:if>
							<xsl:if test="prod/uTrib = ''">
								<uTrib>-</uTrib>
							</xsl:if>

							<xsl:if test="prod/qTrib != ''">
								<qTrib>
									<xsl:value-of select="translate(format-number(prod/qTrib,'#0.0000'),',','.')"></xsl:value-of>
								</qTrib>
							</xsl:if>

							<xsl:if test="prod/qTrib = ''">
								<qTrib>0</qTrib>
							</xsl:if>

							<vUnTrib>
								<xsl:value-of select="translate(format-number(prod/vProd div prod/qCom,'#0.0000'),',','.')"></xsl:value-of>
							</vUnTrib>

							<xsl:if test="prod/vFrete != 0 and prod/vFrete != '' ">
								<vFrete>
									<xsl:value-of select="prod/vFrete"></xsl:value-of>
								</vFrete>
							</xsl:if>
							<xsl:if test="prod/vSeg != 0 and prod/vSeg != '' ">
								<vSeg>
									<xsl:value-of select="prod/vSeg"></xsl:value-of>
								</vSeg>
							</xsl:if>
							<xsl:if test="prod/vDesc != 0 and prod/vSeg != '' ">
								<vDesc>
									<xsl:value-of select="prod/vDesc"></xsl:value-of>
								</vDesc>
							</xsl:if>


							<xsl:if test="prod/DI/nDI != 0 and prod/DI/nDI != '' ">
								<DI>
									<nDI>
										<xsl:value-of select="prod/DI/nDI"></xsl:value-of>
									</nDI>
									<dDI>
										<xsl:value-of select="prod/DI/dDI"></xsl:value-of>
									</dDI>
									<xLocDesemb>
										<xsl:value-of select="prod/DI/xLocDesemb"></xsl:value-of>
									</xLocDesemb>
									<UFDesemb>
										<xsl:value-of select="prod/DI/UFDesemb"></xsl:value-of>
									</UFDesemb>
									<dDesemb>
										<xsl:value-of select="prod/DI/dDesemb"></xsl:value-of>
									</dDesemb>
									<cExportador>
										<xsl:value-of select="prod/DI/cExportador"></xsl:value-of>
									</cExportador>


									<adi>

										<nAdicao>
											<xsl:value-of select="prod/DI/adi/nAdicao"></xsl:value-of>
										</nAdicao>

										<nSeqAdic>
											<xsl:value-of select="prod/DI/adi/nSeqAdic"></xsl:value-of>
										</nSeqAdic>
										<cFabricante>
											<xsl:value-of select="prod/DI/adi/cFabricante"></xsl:value-of>
										</cFabricante>

										<xsl:if test="prod/DI/adi/vDescDI != 0 and prod/DI/adi/vDescDI != '' ">
											<vDescDI>
												<xsl:value-of select="prod/DI/adi/vDescDI"></xsl:value-of>
											</vDescDI>
										</xsl:if>
									</adi>
								</DI>
							</xsl:if>



							<xsl:if test="prod/comb/cProdANP != ''">
								<xsl:if test="number(prod/comb/cProdANP) != 0">
									<comb>
										<cProdANP>
											<xsl:value-of select="prod/comb/cProdANP"></xsl:value-of>
										</cProdANP>
										<CODIF>
											<xsl:value-of select="prod/comb/CODIF"></xsl:value-of>
										</CODIF>
										<qTemp>
											<xsl:value-of select="prod/comb/qTemp"></xsl:value-of>
										</qTemp>
										<CIDE>
											<qBCProd>
												<xsl:value-of select="prod/comb/CIDE/qBCprod"></xsl:value-of>
											</qBCProd>
											<vAliqProd>
												<xsl:value-of select="prod/comb/CIDE/vAliqProd"></xsl:value-of>
											</vAliqProd>
											<vCIDE>
												<xsl:value-of select="prod/comb/CIDE/vCIDE"></xsl:value-of>
											</vCIDE>
										</CIDE>
										<ICMSComb>
											<vBCICMS>
												<xsl:value-of select="prod/comb/ICMSComb/vBCICMS"></xsl:value-of>
											</vBCICMS>
											<vICMS>
												<xsl:value-of select="prod/comb/ICMSComb/vICMS"></xsl:value-of>
											</vICMS>
											<vBCICMSST>
												<xsl:value-of select="prod/comb/ICMSComb/vBCICMSST"></xsl:value-of>
											</vBCICMSST>
											<vICMSST>
												<xsl:value-of select="prod/comb/ICMSComb/vICMSST"></xsl:value-of>
											</vICMSST>
										</ICMSComb>
										<ICMSInter>
											<vBCICMSSTDest>
												<xsl:value-of select="prod/comb/ICMSInter/vBCICMSSTDest"></xsl:value-of>
											</vBCICMSSTDest>
											<vICMSSTDest>
												<xsl:value-of select="prod/comb/ICMSInter/vICMSSTDest"></xsl:value-of>
											</vICMSSTDest>
										</ICMSInter>
										<ICMSCons>
											<vBCICMSSTCons>
												<xsl:value-of select="prod/comb/ICMSCons/vBCICMSSTCons"></xsl:value-of>
											</vBCICMSSTCons>
											<vICMSSTCons>
												<xsl:value-of select="prod/comb/ICMSCons/vICMSSTCons"></xsl:value-of>
											</vICMSSTCons>
											<UFCons>
												<xsl:value-of select="prod/comb/ICMSCons/UFcons"></xsl:value-of>
											</UFCons>
										</ICMSCons>
									</comb>
								</xsl:if>
							</xsl:if>
						</prod>

						<imposto>
							<ICMS>
								<xsl:if test="imposto/ICMS/ICMS00">
									<ICMS00>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS00/orig"></xsl:value-of>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS00/CST,'00')"></xsl:value-of>
										</CST>
										<modBC>
											<xsl:value-of select="imposto/ICMS/ICMS00/modBC"></xsl:value-of>
										</modBC>
										<vBC>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS00/vBC, '#0.00') "></xsl:value-of>
										</vBC>
										<pICMS>
											<xsl:value-of select="imposto/ICMS/ICMS00/pICMS"></xsl:value-of>
										</pICMS>
										<vICMS>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS00/vICMS,'#0.00') "></xsl:value-of>
										</vICMS>
									</ICMS00>
								</xsl:if>
								<xsl:if test="imposto/ICMS/ICMS10">
									<ICMS10>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS10/orig"></xsl:value-of>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS10/CST,'00')"></xsl:value-of>
										</CST>
										<modBC>
											<xsl:value-of select="imposto/ICMS/ICMS10/modBC"></xsl:value-of>
										</modBC>
										<vBC>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS10/vBC, '#0.00') "></xsl:value-of>
										</vBC>
										<pICMS>
											<xsl:value-of select="imposto/ICMS/ICMS10/pICMS"></xsl:value-of>
										</pICMS>
										<vICMS>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS10/vICMS, '#0.00')"></xsl:value-of>
										</vICMS>
										<modBCST>
											<xsl:value-of select="imposto/ICMS/ICMS10/modBCST"></xsl:value-of>
										</modBCST>
										<vBCST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS10/vBCST, '#0.00')"></xsl:value-of>
										</vBCST>
										<pICMSST>
											<xsl:value-of select="imposto/ICMS/ICMS10/pICMSST"></xsl:value-of>
										</pICMSST>
										<vICMSST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS10/vICMSST, '#0.00')"></xsl:value-of>
										</vICMSST>
									</ICMS10>
								</xsl:if>
								<xsl:if test="imposto/ICMS/ICMS20">
									<ICMS20>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS20/orig"></xsl:value-of>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS20/CST,'00')"></xsl:value-of>
										</CST>
										<modBC>
											<xsl:value-of select="imposto/ICMS/ICMS20/modBC"></xsl:value-of>
										</modBC>
										<pRedBC>
											<xsl:value-of select="imposto/ICMS/ICMS20/pRedBC"></xsl:value-of>
										</pRedBC>
										<vBC>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS20/vBC, '#0.00')"></xsl:value-of>
										</vBC>
										<pICMS>
											<xsl:value-of select="imposto/ICMS/ICMS20/pICMS"></xsl:value-of>
										</pICMS>
										<vICMS>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS20/vICMS, '#0.00')"></xsl:value-of>
										</vICMS>
									</ICMS20>
								</xsl:if>
								<xsl:if test="imposto/ICMS/ICMS40">
									<ICMS40>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS40/orig"></xsl:value-of>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS40/CST,'00')"></xsl:value-of>
										</CST>
									</ICMS40>
								</xsl:if>
								<xsl:if test="imposto/ICMS/ICMS41">
									<ICMS40>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS41/orig"></xsl:value-of>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS41/CST,'00')"></xsl:value-of>
										</CST>
									</ICMS40>
								</xsl:if>
								<xsl:if test="imposto/ICMS/ICMS50">
									<ICMS40>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS50/orig"></xsl:value-of>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS50/CST,'00')"></xsl:value-of>
										</CST>
									</ICMS40>
								</xsl:if>
								<xsl:if test="imposto/ICMS/ICMS51">
									<ICMS51>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS51/orig"></xsl:value-of>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS51/CST,'00')"></xsl:value-of>
										</CST>
										<modBC>
											<xsl:value-of select="imposto/ICMS/ICMS51/modBC"></xsl:value-of>
										</modBC>
										<pRedBC>
											<xsl:value-of select="imposto/ICMS/ICMS51/pRedBC"></xsl:value-of>
										</pRedBC>
										<vBC>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS51/vBC, '#0.00')"></xsl:value-of>
										</vBC>
										<pICMS>
											<xsl:value-of select="imposto/ICMS/ICMS51/pICMS"></xsl:value-of>
										</pICMS>
										<vICMS>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS51/vICMS, '#0.00')"></xsl:value-of>
										</vICMS>
									</ICMS51>
								</xsl:if>
								<xsl:if test="imposto/ICMS/ICMS60">
									<ICMS60>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS60/orig"></xsl:value-of>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS60/CST,'00')"></xsl:value-of>
										</CST>
										<xsl:if test="number(imposto/ICMS/ICMS60/vBCST) = 0">
											<vBCST>0</vBCST>
										</xsl:if>
										<xsl:if test="number(imposto/ICMS/ICMS60/vBCST) != 0">
											<vBCST>
												<xsl:value-of select="format-number(imposto/ICMS/ICMS60/vBCST, '#0.00')"></xsl:value-of>
											</vBCST>
										</xsl:if>
										<xsl:if test="number(imposto/ICMS/ICMS60/vICMSST) = 0">
											<vICMSST>0</vICMSST>
										</xsl:if>
										<xsl:if test="number(imposto/ICMS/ICMS60/vICMSST) != 0">
											<vICMSST>
												<xsl:value-of select="format-number(imposto/ICMS/ICMS60/vICMSST, '#0.00')"></xsl:value-of>
											</vICMSST>
										</xsl:if>
									</ICMS60>
								</xsl:if>
								<xsl:if test="imposto/ICMS/ICMS90">
									<ICMS90>
										<orig>
											<xsl:value-of select="imposto/ICMS/ICMS90/orig"></xsl:value-of>
										</orig>
										<CST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS90/CST,'00')"></xsl:value-of>
										</CST>
										
										<xsl:if test="imposto/ICMS/ICMS90/vBC != '' ">
										<modBC>
											<xsl:value-of select="imposto/ICMS/ICMS90/modBC"></xsl:value-of>
										</modBC>

										<xsl:if test="imposto/ICMS/ICMS90/pRedBC != '' ">
										<pRedBC>
											<xsl:value-of select="imposto/ICMS/ICMS90/pRedBC"></xsl:value-of>
										</pRedBC>
										</xsl:if>

										<vBC>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS90/vBC, '#0.00') "></xsl:value-of>
										</vBC>
										<pICMS>
											<xsl:value-of select="imposto/ICMS/ICMS90/pICMS"></xsl:value-of>
										</pICMS>
										<vICMS>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS90/vICMS, '#0.00')"></xsl:value-of>
										</vICMS>

										</xsl:if>
										<xsl:if test="imposto/ICMS/ICMS90/vBCST != '' ">
										<modBCST>
											<xsl:value-of select="imposto/ICMS/ICMS90/modBCST"></xsl:value-of>
										</modBCST>
										<vBCST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS90/vBCST, '#0.00')"></xsl:value-of>
										</vBCST>
										<pICMSST>
											<xsl:value-of select="imposto/ICMS/ICMS90/pICMSST"></xsl:value-of>
										</pICMSST>
										<vICMSST>
											<xsl:value-of select="format-number(imposto/ICMS/ICMS90/vICMSST, '#0.00')"></xsl:value-of>
										</vICMSST>
										</xsl:if>

									</ICMS90>
								</xsl:if>
							</ICMS>

							<xsl:if test="imposto/IPI/cEnq">
								<IPI>
									<xsl:if test="imposto/IPI/clEnq != '' and imposto/IPI/clEnq != 0">
										<clEnq>
											<xsl:value-of select="imposto/IPI/clEnq"></xsl:value-of>
										</clEnq>
									</xsl:if>
									<xsl:if test="imposto/IPI/CNPJProd != '' and imposto/IPI/CNPJProd != 0">
										<CNPJProd>
											<xsl:value-of select="imposto/IPI/CNPJProd"></xsl:value-of>
										</CNPJProd>
									</xsl:if>
									<xsl:if test="imposto/IPI/cSelo != '' and imposto/IPI/cSelo != 0 ">
										<cSelo>
											<xsl:value-of select="imposto/IPI/cSelo"></xsl:value-of>
										</cSelo>
									</xsl:if>
									<xsl:if test="imposto/IPI/qSelo != '' and imposto/IPI/qSelo != 0">
										<qSelo>
											<xsl:value-of select="imposto/IPI/qSelo"></xsl:value-of>
										</qSelo>
									</xsl:if>
									<cEnq>999</cEnq>


									<xsl:if test="imposto/IPI/IPITrib/CST != ''and (imposto/IPI/IPITrib/CST = 00 or imposto/IPI/IPITrib/CST = 49 or imposto/IPI/IPITrib/CST = 50 or imposto/IPI/IPITrib/CST = 99)">
										<IPITrib>
											<CST>
												<xsl:value-of select="imposto/IPI/IPITrib/CST "></xsl:value-of>
											</CST>
											<vBC>
												<xsl:value-of select="format-number(imposto/IPI/IPITrib/vBC, '#0.00') "></xsl:value-of>
											</vBC>
											<xsl:if test="imposto/IPI/IPITrib/pIPI != '' ">
												<pIPI>
													<xsl:value-of select="format-number(imposto/IPI/IPITrib/pIPI , '#0.00') "></xsl:value-of>
												</pIPI>
												<vIPI>
													<xsl:value-of select="format-number(imposto/IPI/IPITrib/vIPI, '#0.00') "></xsl:value-of>
												</vIPI>
											</xsl:if>
											<xsl:if test="imposto/IPI/IPITrib/qUnid != '' ">
												<qUnid>
													<xsl:value-of select="format-number(imposto/IPI/IPITrib/qUnid, '#0.0000') "></xsl:value-of>
												</qUnid>
												<vUnid>
													<xsl:value-of select="format-number(imposto/IPI/IPITrib/vUnid, '#0.0000') "></xsl:value-of>
												</vUnid>
											</xsl:if>
										</IPITrib>
									</xsl:if>
									<xsl:if test="imposto/IPI/IPITrib/CST != '' and imposto/IPI/IPITrib/CST != 0 and (imposto/IPI/IPITrib/CST = '01' or imposto/IPI/IPITrib/CST = '02' or imposto/IPI/IPITrib/CST = 03 or imposto/IPI/IPITrib/CST  = '04'     or imposto/IPI/IPITrib/CST = '05' or imposto/IPI/IPITrib/CST = '51' or imposto/IPI/IPITrib/CST = '52' or imposto/IPI/IPITrib/CST = '53' or imposto/IPI/IPITrib/CST = '54' or imposto/IPI/IPITrib/CST = '55')">
										<IPINT>
											<CST>
												<xsl:value-of select="format-number(imposto/IPI/IPITrib/CST,'00')"></xsl:value-of>
											</CST>
										</IPINT>
									</xsl:if>
								</IPI>
							</xsl:if>

							<xsl:if test="imposto/II/vII != '' and number(imposto/II/vII) != 0">
								<II>
									<vBC>
										<xsl:value-of select="imposto/II/vBC"></xsl:value-of>
									</vBC>
									<vDespAdu>
										<xsl:value-of select="imposto/II/vDespAdu"></xsl:value-of>
									</vDespAdu>
									<vII>
										<xsl:value-of select="imposto/II/vII"></xsl:value-of>
									</vII>
									<vIOF>
										<xsl:value-of select="imposto/II/vIOF"></xsl:value-of>
									</vIOF>
								</II>
							</xsl:if>

							<PIS>
								<xsl:if test="imposto/PIS/PISPadr != '' and number(imposto/PIS/PISPadr/CST) != 0 and (imposto/PIS/PISPadr/CST= 01 or imposto/PIS/PISPadr/CST = 02) ">
									<!--PIS  tributado pela aliquota  (CST 01) -->
									<PISAliq>
										<CST>
											<xsl:value-of select="format-number(imposto/PIS/PISPadr/CST,'00')"></xsl:value-of>
										</CST>
										<vBC>
											<xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/vBC,'#0.00'),',','.')"></xsl:value-of>
										</vBC>
										<pPIS>
											<xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/pPIS,'#0.00'),',','.')"></xsl:value-of>
										</pPIS>
										<vPIS>
											<xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/vPIS,'#0.00'),',','.')"></xsl:value-of>
										</vPIS>
									</PISAliq>
								</xsl:if>

								<!--PIS  tributado por qtde(CST 03) -->

								<xsl:if test="imposto/PIS/PISPadr != '' and number(imposto/PIS/PISPadr/CST) != 0 and imposto/PIS/PISPadr/CST = 03 ">
									<PISQtde>
										<CST>
											<xsl:value-of select="format-number(imposto/PIS/PISPadr/CST,'00')"></xsl:value-of>
										</CST>
										<vBC>
											<xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/vBC,'#0.00'),',','.')"></xsl:value-of>
										</vBC>
										<pPIS>
											<xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/pPIS,'#0.00'),',','.')"></xsl:value-of>
										</pPIS>
										<vPIS>
											<xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/vPIS,'#0.00'),',','.')"></xsl:value-of>
										</vPIS>
									</PISQtde>
								</xsl:if>



								<!--PIS nao tributado (CST 04, 06, 07, 08 ou 9 -->

								<xsl:if test="imposto/PIS/PISPadr != '' and number(imposto/PIS/PISPadr/CST) !=0 and (imposto/PIS/PISAliq/CST = 04 or imposto/PIS/PISPadr/CST = 06 or imposto/PIS/PISPadr/CST = 07 or imposto/PIS/PISPadr/CST = 08 or imposto/PIS/PISPadr/CST = 09)">
									<PISNT>
										<CST>
											<xsl:value-of select="format-number(imposto/PIS/PISPadr/CST,'00')"></xsl:value-of>
										</CST>
									</PISNT>
								</xsl:if>

								<!-- PIS outras operacoes -->
								<xsl:if test="imposto/PIS/PISPadr != '' and number(imposto/PIS/PISPadr/CST) != 0 and imposto/PIS/PISPadr/CST = 99 ">
									<PISOutr>
										<CST>
											<xsl:value-of select="format-number(imposto/PIS/PISPadr/CST,'00')"></xsl:value-of>
										</CST>


										<xsl:if test="imposto/PIS/PISPadr/vAliqProd != '' and number(imposto/PIS/PISPadr/vAliqProd) != 0  ">
											<qBCProd>
												<xsl:value-of select="imposto/PIS/PISPadr/qBCProd"></xsl:value-of>
											</qBCProd>
											<vAliqProd>
												<xsl:value-of select="imposto/PIS/PISPadr/vAliqProd"></xsl:value-of>
											</vAliqProd>
										</xsl:if>

										<xsl:if test="imposto/PIS/PISPadr/pPIS!= ''">
											<vBC>
												<xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/vBC,'#0.00'),',','.')"></xsl:value-of>
											</vBC>
											<pPIS>
												<xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/pPIS,'#0.00'),',','.')"></xsl:value-of>
											</pPIS>
										</xsl:if>

										<vPIS>
											<xsl:value-of select="translate(format-number(imposto/PIS/PISPadr/vPIS,'#0.00'),',','.')"></xsl:value-of>
										</vPIS>
									</PISOutr>
								</xsl:if>

								<xsl:if test="imposto/PIS/PISPadr/CST = '' ">
									<PISNT>
										<CST>08</CST>
									</PISNT>
								</xsl:if>
							</PIS>


							<!--PIS Outras operacoes(CST 99) -->




							<xsl:if test="imposto/PISST/vPIS != '' and number(imposto/PISST/vPIS) != 0 ">
								<PISST>
									<vBC>
										<xsl:value-of select="imposto/PISST/vBC"></xsl:value-of>
									</vBC>
									<pPIS>
										<xsl:value-of select="imposto/PISST/pPIS"></xsl:value-of>
									</pPIS>
									<qBCProd>
										<xsl:value-of select="imposto/PISST/qBCProd"></xsl:value-of>
									</qBCProd>
									<vAliqProd>
										<xsl:value-of select="imposto/PISST/vAliqProd"></xsl:value-of>
									</vAliqProd>
									<vPIS>
										<xsl:value-of select="imposto/PISST/vPIS"></xsl:value-of>
									</vPIS>
								</PISST>
							</xsl:if>




							<COFINS>
								<!-- COFINS por aliqutoca (CST 01 ou 02) -->
								<xsl:if test="imposto/COFINS/COFINSPadr != '' and number(imposto/COFINS/COFINSPadr/CST) != 0 and (number(imposto/COFINS/COFINSPadr/CST) = 01 or number(imposto/COFINS/COFINSPadr/CST) = 02)">
									<COFINSAliq>
										<CST>
											<xsl:value-of select="format-number(imposto/COFINS/COFINSPadr/CST,'00')"></xsl:value-of>
										</CST>
										<vBC>
											<xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/vBC,'#0.00'),',','.')"></xsl:value-of>
										</vBC>
										<pCOFINS>
											<xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/pCOFINS,'#0.00'),',','.')"></xsl:value-of>
										</pCOFINS>
										<vCOFINS>
											<xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/vCOFINS,'#0.00'),',','.')"></xsl:value-of>
										</vCOFINS>
									</COFINSAliq>
								</xsl:if>
								<!-- 
								COFINS por quantidade (cst 03)-->

								<xsl:if test="imposto/COFINS/COFINSPadr != '' and number(imposto/COFINS/COFINSPadr/CST) != 0 and (number(imposto/COFINS/COFINSPadr/CST) = 03)">
									<COFINSQtde>
										<CST>
											<xsl:value-of select="format-number(imposto/COFINS/COFINSPadr/CST,'00')"></xsl:value-of>
										</CST>
										<qBCProd>
											<xsl:value-of select="imposto/COFINS/COFINSPadr/qBCProd"></xsl:value-of>
										</qBCProd>
										<vAliqProd>
											<xsl:value-of select="imposto/COFINS/COFINSPadr/vAliqProd"></xsl:value-of>
										</vAliqProd>
										<vCOFINS>
											<xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/vCOFINS,'#0.00'),',','.')"></xsl:value-of>
										</vCOFINS>
									</COFINSQtde>
								</xsl:if>

								<!--cofins nao tributado (cst 04, 06, 07, 08 ou 09-->

								<xsl:if test="imposto/COFINS/COFINSPadr/CST != '' and number(imposto/COFINS/COFINSPadr/CST) != 0 and (number(imposto/COFINS/COFINSPadr/CST) = 04 or number(imposto/COFINS/COFINSPadr/CST) = 06 or number(imposto/COFINS/COFINSPadr/CST) = 07 or number(imposto/COFINS/COFINSPadr/CST) = 08 or number(imposto/COFINS/COFINSPadr/CST) = 09)">
									<COFINSNT>
										<CST>
											<xsl:value-of select="format-number(imposto/COFINS/COFINSPadr/CST,'00')"></xsl:value-of>
										</CST>
									</COFINSNT>
								</xsl:if>

								<xsl:if test="imposto/COFINS/COFINSPadr/CST = '' ">
									<COFINSNT>
										<CST>08</CST>
									</COFINSNT>
								</xsl:if>

								<xsl:if test="imposto/COFINS/COFINSPadr/CST != '' and number(imposto/COFINS/COFINSPadr/CST) != 0 and (number(imposto/COFINS/COFINSPadr/CST) = 99)">
									<COFINSOutr>
										<CST>
											<xsl:value-of select="format-number(imposto/COFINS/COFINSPadr/CST,'00')"></xsl:value-of>
										</CST>
										<vBC>
											<xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/vBC,'#0.00'),',','.')"></xsl:value-of>
										</vBC>
										<pCOFINS>
											<xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/pCOFINS,'#0.00'),',','.')"></xsl:value-of>
										</pCOFINS>
										<vCOFINS>
											<xsl:value-of select="translate(format-number(imposto/COFINS/COFINSPadr/vCOFINS,'#0.00'),',','.')"></xsl:value-of>
										</vCOFINS>
									</COFINSOutr>
								</xsl:if>
							</COFINS>


							<xsl:if test="imposto/COFINSST/vCOFINS != '' and number(imposto/COFINSST/vCOFINS) != 0 ">
								<COFINSST>
									<vBC>
										<xsl:value-of select="imposto/COFINSST/vBC"></xsl:value-of>
									</vBC>
									<pCOFINS>
										<xsl:value-of select="imposto/COFINSST/pCOFINS"></xsl:value-of>
									</pCOFINS>
									<qBCProd>
										<xsl:value-of select="imposto/COFINSST/qBCProd"></xsl:value-of>
									</qBCProd>
									<vAliqProd>
										<xsl:value-of select="imposto/COFINSST/vAliqProd"></xsl:value-of>
									</vAliqProd>
									<vCOFINS>
										<xsl:value-of select="imposto/COFINSST/vCOFINS"></xsl:value-of>
									</vCOFINS>
								</COFINSST>
							</xsl:if>

							<xsl:if test="imposto/ISSQN/vISSQN != '' and number(imposto/ISSQN/vISSQN) != 0 ">
								<ISSQN>
									<vBC>
										<xsl:value-of select="imposto/ISSQN/vBC"></xsl:value-of>
									</vBC>
									<vAliq>
										<xsl:value-of select="imposto/ISSQN/vAliq"></xsl:value-of>
									</vAliq>
									<vISSQN>
										<xsl:value-of select="imposto/ISSQN/vISSQN"></xsl:value-of>
									</vISSQN>
									<cMunFG>
										<xsl:value-of select="imposto/ISSQN/cMunFG"></xsl:value-of>
									</cMunFG>
									<cListServ>
										<xsl:value-of select="imposto/ISSQN/cListServ"></xsl:value-of>
									</cListServ>
								</ISSQN>
							</xsl:if>
						</imposto>

						<xsl:if test="infAdProd != ''">
							<infAdProd>
								<xsl:value-of select="infAdProd"></xsl:value-of>
							</infAdProd>
						</xsl:if>
					</det>
				</xsl:for-each>




				<total>
					<ICMSTot>
						<xsl:if test="NFe/infNFe/total/ICMSTot/vBC = '' ">
							<vBC>0.00</vBC>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vBC != '' ">
							<vBC>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vBC,'#0.00'),',','.')"></xsl:value-of>
							</vBC>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vICMS = ''">
							<vICMS>0.00</vICMS>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vICMS != ''">
							<vICMS>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vICMS,'#0.00'),',','.')"></xsl:value-of>
							</vICMS>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vBCST = ''">
							<vBCST>0.00</vBCST>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vBCST != ''">
							<vBCST>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vBCST,'#0.00'),',','.')"></xsl:value-of>
							</vBCST>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vST = ''">
							<vST>0.00</vST>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vST != ''">
							<vST>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vST,'#0.00'),',','.')"></xsl:value-of>
							</vST>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vProd = '' ">
							<vProd>0.00</vProd>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vProd != '' ">
							<vProd>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vProd,'#0.00'),',','.')"></xsl:value-of>
							</vProd>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vFrete = ''">
							<vFrete>0.00</vFrete>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vFrete != ''">
							<vFrete>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vFrete,'#0.00'),',','.')"></xsl:value-of>
							</vFrete>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vSeg = ''">
							<vSeg>0.00</vSeg>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vSeg!= ''">
							<vSeg>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vSeg,'#0.00'),',','.')"></xsl:value-of>
							</vSeg>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vDesc = ''">
							<vDesc>0.00</vDesc>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vDesc != ''">
							<vDesc>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vDesc,'#0.00'),',','.')"></xsl:value-of>
							</vDesc>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vII = ''">
							<vII>0.00</vII>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vII != ''">
							<vII>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vII,'#0.00'),',','.')"></xsl:value-of>
							</vII>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vIPI = ''">
							<vIPI>0.00</vIPI>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vIPI != ''">
							<vIPI>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vIPI,'#0.00'),',','.')"></xsl:value-of>
							</vIPI>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vPIS = ''">
							<vPIS>0.00</vPIS>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vPIS != ''">
							<vPIS>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vPIS,'#0.00'),',','.')"></xsl:value-of>
							</vPIS>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vCOFINS = ''">
							<vCOFINS>0.00</vCOFINS>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vCOFINS != ''">
							<vCOFINS>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vCOFINS,'#0.00'),',','.')"></xsl:value-of>
							</vCOFINS>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vOutro = ''">
							<vOutro>0.00</vOutro>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vOutro != ''">
							<vOutro>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vOutro,'#0.00'),',','.')"></xsl:value-of>
							</vOutro>
						</xsl:if>

						<xsl:if test="NFe/infNFe/total/ICMSTot/vNF != ''">
							<vNF>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ICMSTot/vNF,'#0.00'),',','.')"></xsl:value-of>
							</vNF>
						</xsl:if>
					</ICMSTot>
					<xsl:if test="NFe/infNFe/total/ISSQNtot/vServ != 0 and (NFe/infNFe/total/ISSQNtot/vServ != '' or NFe/infNFe/total/ISSQNtot/vBC != '')">
						<ISSQNtot>
							<vServ>
								<xsl:value-of select="translate(format-number(NFe/infNFe/total/ISSQNtot/vServ,'#0.00'),',','.')"></xsl:value-of>
							</vServ>
							<xsl:if test="NFe/infNFe/total/ISSQNtot/vBC != '' ">
								<vBC>
									<xsl:value-of select="translate(format-number(NFe/infNFe/total/ISSQNtot/vBC,'#0.00'),',','.')"></xsl:value-of>
								</vBC>
							</xsl:if>
							<xsl:if test="NFe/infNFe/total/ISSQNtotTot/vISSrv != ''">
								<vISS>
									<xsl:value-of select="translate(format-number(NFe/infNFe/total/ISSQNtot/vISS,'#0.00'),',','.')"></xsl:value-of>
								</vISS>
							</xsl:if>
							<xsl:if test="NFe/infNFe/total/ISSQNtot/vPIS != '' ">
								<vPIS>
									<xsl:value-of select="translate(format-number(NFe/infNFe/total/ISSQNtot/vPIS,'#0.00'),',','.')"></xsl:value-of>
								</vPIS>
							</xsl:if>
							<xsl:if test="NFe/infNFe/total/ISSQNtot/vCOFINS != ''">
								<vCOFINS>
									<xsl:value-of select="translate(format-number(NFe/infNFe/total/ISSQNtot/vCOFINS,'#0.00'),',','.')"></xsl:value-of>
								</vCOFINS>
							</xsl:if>
						</ISSQNtot>
					</xsl:if>
				</total>
				<transp>
					<modFrete>
						<xsl:value-of select="NFe/infNFe/transp/modFrete"></xsl:value-of>
					</modFrete>
					<xsl:if test="NFe/infNFe/transp/transporta/xNome  != ''">
						<transporta>
							<xsl:if test="NFe/infNFe/transp/transporta/CNPJ != ''">
								<CNPJ>
									<xsl:value-of select="NFe/infNFe/transp/transporta/CNPJ"></xsl:value-of>
								</CNPJ>
							</xsl:if>
							<xsl:if test="NFe/infNFe/transp/transporta/CPF != ''">
								<CPF>
									<xsl:value-of select="translate(NFe/infNFe/transp/transporta/CPF,'-','')"></xsl:value-of>
								</CPF>
							</xsl:if>
							<xNome>
								<xsl:value-of select="NFe/infNFe/transp/transporta/xNome"></xsl:value-of>
							</xNome>
							<xsl:if test="NFe/infNFe/transp/transporta/IE != ''">
								<IE>
									<xsl:value-of select="NFe/infNFe/transp/transporta/IE"></xsl:value-of>
								</IE>
							</xsl:if>
							<xsl:if test="NFe/infNFe/transp/transporta/xEnder != ''">
								<xEnder>
									<xsl:value-of select="NFe/infNFe/transp/transporta/xEnder"></xsl:value-of>
								</xEnder>
							</xsl:if>
							<xsl:if test="NFe/infNFe/transp/transporta/xMun != ''">
								<xMun>
									<xsl:value-of select="NFe/infNFe/transp/transporta/xMun"></xsl:value-of>
								</xMun>
							</xsl:if>
							<UF>
								<xsl:value-of select="NFe/infNFe/transp/transporta/UF"></xsl:value-of>
							</UF>
						</transporta>
					</xsl:if>

					<xsl:if test="number(NFe/infNFe/transp/retTransp/vBCRet) != 0 and NFe/infNFe/transp/retTransp/vBCRet != '' ">
						<retTransp>
							<vServ>
								<xsl:value-of select="NFe/infNFe/transp/retTransp/vServ"></xsl:value-of>
							</vServ>
							<vBCRet>
								<xsl:value-of select="NFe/infNFe/transp/retTransp/vBCRet"></xsl:value-of>
							</vBCRet>
							<pICMSRet>
								<xsl:value-of select="NFe/infNFe/transp/retTransp/pICMSRet"></xsl:value-of>
							</pICMSRet>
							<vICMSRet>
								<xsl:value-of select="NFe/infNFe/transp/retTransp/vICMSRet"></xsl:value-of>
							</vICMSRet>
							<CFOP>
								<xsl:value-of select="NFe/infNFe/transp/retTransp/CFOP"></xsl:value-of>
							</CFOP>
							<cMunFG>
								<xsl:value-of select="NFe/infNFe/transp/retTransp/cMunFG"></xsl:value-of>
							</cMunFG>
						</retTransp>
					</xsl:if>
					<xsl:if test="NFe/infNFe/transp/veicTransp/placa != '' and NFe/infNFe/transp/veicTransp/UF != '' ">
						<veicTransp>
							<placa>
								<xsl:value-of select="NFe/infNFe/transp/veicTransp/placa"></xsl:value-of>
							</placa>
							<UF>
								<xsl:value-of select="NFe/infNFe/transp/veicTransp/UF"></xsl:value-of>
							</UF>
							<xsl:if test="NFe/infNFe/transp/veicTransp/RNTC != ''">
								<RNTC>
									<xsl:value-of select="NFe/infNFe/transp/veicTransp/RNTC"></xsl:value-of>
								</RNTC>
							</xsl:if>
						</veicTransp>
					</xsl:if>

					<xsl:if test="NFe/infNFe/transp/reboque/placa != ''">
						<reboque>
							<placa>
								<xsl:value-of select="NFe/infNFe/transp/reboque/placa"></xsl:value-of>
							</placa>
							<UF>
								<xsl:value-of select="NFe/infNFe/transp/reboque/UF"></xsl:value-of>
							</UF>
							<RNTC>
								<xsl:value-of select="NFe/infNFe/transp/reboque/RNTC"></xsl:value-of>
							</RNTC>
						</reboque>
					</xsl:if>

					<xsl:for-each select="NFe/infNFe/transp/vol">
						<vol>
							<xsl:if test="qVol != ''">
								<qVol>
									<xsl:value-of select="qVol"></xsl:value-of>
								</qVol>
							</xsl:if>
							<xsl:if test="esp != ''">
								<esp>
									<xsl:value-of select="esp"></xsl:value-of>
								</esp>
							</xsl:if>
							<xsl:if test="marca != ''">
								<marca>
									<xsl:value-of select="marca"></xsl:value-of>
								</marca>
							</xsl:if>
							<xsl:if test="nVol != ''">
								<nVol>
									<xsl:value-of select="nVol"></xsl:value-of>
								</nVol>
							</xsl:if>
							<xsl:if test="pesoL != ''">
								<pesoL>
									<xsl:value-of select="format-number(number(pesoL),'#0.000')"></xsl:value-of>
								</pesoL>
							</xsl:if>
							<xsl:if test="pesoB != ''">
								<pesoB>
									<xsl:value-of select="format-number(number(pesoB),'#0.000')"></xsl:value-of>
								</pesoB>
							</xsl:if>
							<xsl:if test="lacres/nLacre != ''">
								<lacres>
									<nLacre>
										<xsl:value-of select="lacres/nLacre"></xsl:value-of>
									</nLacre>
								</lacres>
							</xsl:if>
						</vol>
					</xsl:for-each>
				</transp>



				<cobr>
					<xsl:if test="NFe/infNFe/cobr/fat/nFat != ''">
						<fat>
							<nFat>
								<xsl:value-of select="NFe/infNFe/cobr/fat/nFat"></xsl:value-of>
							</nFat>
							<xsl:if test="NFe/infNFe/cobr/fat/vOrig != 0">
								<vOrig>
									<xsl:value-of select="translate(format-number(NFe/infNFe/cobr/fat/vOrig,'#0.00'),',','.')"></xsl:value-of>
								</vOrig>
							</xsl:if>
							<xsl:if test="NFe/infNFe/cobr/fat/vDesc != 0">
								<vDesc>
									<xsl:value-of select="translate(format-number(NFe/infNFe/cobr/fat/vDesc,'#0.00'),',','.')"></xsl:value-of>
								</vDesc>
							</xsl:if>
							<xsl:if test="NFe/infNFe/cobr/fat/vliq != 0">
								<vLiq>
									<xsl:value-of select="translate(format-number(NFe/infNFe/cobr/fat/vLiq,'#0.00'),',','.')"></xsl:value-of>
								</vLiq>
							</xsl:if>
						</fat>
					</xsl:if>

					<xsl:for-each select="NFe/infNFe/cobr/dup">
						<xsl:if test="nDup != '' and vDup != '' and nDup !=0 and vDup != 0">
							<dup>
								<nDup>
									<xsl:value-of select="nDup"></xsl:value-of>
								</nDup>
								<dVenc>
									<xsl:value-of select="dVenc"></xsl:value-of>
								</dVenc>
								<vDup>
									<xsl:value-of select="translate(format-number(vDup,'#0.00'),',','.')"></xsl:value-of>
								</vDup>
							</dup>
						</xsl:if>
					</xsl:for-each>
				</cobr>
				<xsl:if test="NFe/infNFe/infAdic/infAdFisco != '' or NFe/infNFe/infAdic/infCpl != ''">
					<infAdic>
						<xsl:if test="NFe/infNFe/infAdic/infAdFisco != ''">
							<infAdFisco>
								<xsl:value-of select="NFe/infNFe/infAdic/infAdFisco"></xsl:value-of>
							</infAdFisco>
						</xsl:if>
						<xsl:if test="NFe/infNFe/infAdic/infCpl != ''">
							<infCpl>
								<xsl:value-of select="NFe/infNFe/infAdic/infCpl"></xsl:value-of>
							</infCpl>
						</xsl:if>
					</infAdic>
				</xsl:if>
			</infNFe>
		</NFe>
	</xsl:template>
</xsl:stylesheet>
<!-- Stylus Studio meta-information - (c) 2004-2006. Progress Software Corporation. All rights reserved.
<metaInformation>
<scenarios ><scenario default="yes" name="XML Canonico 1.10 2.xml" userelativepaths="no" externalpreview="no" url="file:///c:/Projetos/Fisconet4/XML Canonico 1.10 2.xml" htmlbaseurl="" outputurl="" processortype="msxmldotnet2" useresolver="no" profilemode="0" profiledepth="" profilelength="" urlprofilexml="" commandline="" additionalpath="" additionalclasspath="" postprocessortype="none" postprocesscommandline="" postprocessadditionalpath="" postprocessgeneratedext="" validateoutput="no" validator="internal" customvalidator=""/></scenarios><MapperMetaTag><MapperInfo srcSchemaPathIsRelative="yes" srcSchemaInterpretAsXML="no" destSchemaPath="..\..\PL_005c\nfe_v1.10.xsd" destSchemaRoot="NFe" destSchemaPathIsRelative="yes" destSchemaInterpretAsXML="no" ><SourceSchema srcSchemaPath="..\..\XML Canonico 1.10 2.xml" srcSchemaRoot="NFe" AssociatedInstance="" loaderFunction="document" loaderFunctionUsesURI="no"/></MapperInfo><MapperBlockPosition><template match="/"><block path="NFe/infNFe/ide/cNF/xsl:value&#x2D;of" x="242" y="96"/><block path="NFe/infNFe/ide/serie/xsl:value&#x2D;of" x="165" y="141"/><block path="NFe/infNFe/ide/nNF/xsl:value&#x2D;of" x="282" y="166"/><block path="NFe/infNFe/ide/tpNF/xsl:value&#x2D;of" x="164" y="197"/><block path="NFe/infNFe/ide/tpImp/xsl:value&#x2D;of" x="242" y="249"/><block path="NFe/infNFe/emit/enderEmit/xsl:if/!=[0]" x="236" y="76"/><block path="NFe/infNFe/emit/enderEmit/xsl:if" x="282" y="77"/><block path="NFe/infNFe/emit/enderEmit/xsl:if[1]/!=[0]" x="156" y="76"/><block path="NFe/infNFe/emit/enderEmit/xsl:if[1]" x="202" y="77"/><block path="NFe/infNFe/emit/enderEmit/xsl:if[2]/=[0]" x="36" y="76"/><block path="NFe/infNFe/emit/enderEmit/xsl:if[2]" x="82" y="77"/><block path="NFe/infNFe/emit/enderEmit/xsl:if[3]/!=[0]" x="196" y="45"/><block path="NFe/infNFe/emit/enderEmit/xsl:if[3]" x="242" y="46"/><block path="NFe/infNFe/emit/enderEmit/xsl:if[3]/fone/xsl:value&#x2D;of" x="42" y="77"/><block path="NFe/infNFe/emit/xsl:if/!=[0]" x="36" y="199"/><block path="NFe/infNFe/emit/xsl:if" x="82" y="201"/><block path="NFe/infNFe/dest/xsl:if/!=[0]" x="0" y="199"/><block path="NFe/infNFe/dest/xsl:if" x="42" y="201"/><block path="NFe/infNFe/dest/xsl:if[1]/!=[0]" x="116" y="168"/><block path="NFe/infNFe/dest/xsl:if[1]" x="162" y="170"/><block path="NFe/infNFe/dest/xsl:if[1]/CPF/xsl:value&#x2D;of" x="242" y="163"/><block path="NFe/infNFe/dest/enderDest/xsl:if/!=[0]" x="128" y="89"/><block path="NFe/infNFe/dest/enderDest/xsl:if" x="233" y="89"/><block path="NFe/infNFe/dest/enderDest/xsl:if[1]/!=[0]" x="158" y="149"/><block path="NFe/infNFe/dest/enderDest/xsl:if[1]" x="222" y="164"/><block path="NFe/infNFe/dest/enderDest/xsl:if[2]/=[0]" x="120" y="243"/><block path="NFe/infNFe/dest/enderDest/xsl:if[2]" x="226" y="234"/><block path="NFe/infNFe/dest/enderDest/xsl:if[3]/!=[0]" x="76" y="45"/><block path="NFe/infNFe/dest/enderDest/xsl:if[3]" x="122" y="46"/><block path="NFe/infNFe/xsl:if/!=[0]" x="102" y="255"/><block path="NFe/infNFe/xsl:if" x="169" y="256"/><block path="NFe/infNFe/xsl:if/retirada/xsl:if/!=[0]" x="76" y="168"/><block path="NFe/infNFe/xsl:if/retirada/xsl:if" x="122" y="170"/><block path="NFe/infNFe/xsl:if/retirada/xsl:if[1]/!=[0]" x="48" y="145"/><block path="NFe/infNFe/xsl:if/retirada/xsl:if[1]" x="140" y="124"/><block path="NFe/infNFe/xsl:if/retirada/xsl:if[2]/=[0]" x="61" y="187"/><block path="NFe/infNFe/xsl:if/retirada/xsl:if[2]" x="173" y="184"/><block path="NFe/infNFe/xsl:if[1]/!=[0]" x="236" y="199"/><block path="NFe/infNFe/xsl:if[1]" x="282" y="201"/><block path="NFe/infNFe/xsl:if[1]/entrega/xsl:if/!=[0]" x="236" y="138"/><block path="NFe/infNFe/xsl:if[1]/entrega/xsl:if" x="282" y="139"/><block path="NFe/infNFe/xsl:if[1]/entrega/xsl:if[1]/!=[0]" x="156" y="138"/><block path="NFe/infNFe/xsl:if[1]/entrega/xsl:if[1]" x="202" y="139"/><block path="NFe/infNFe/xsl:if[1]/entrega/xsl:if[2]/=[0]" x="116" y="138"/><block path="NFe/infNFe/xsl:if[1]/entrega/xsl:if[2]" x="162" y="139"/><block path="NFe/infNFe/xsl:for&#x2D;each" x="202" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if/!=[0]" x="36" y="45"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if" x="82" y="46"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[1]/!=[0]" x="0" y="45"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[1]" x="42" y="46"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/qCom/xsl:value&#x2D;of" x="242" y="15"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/qCom/xsl:value&#x2D;of/format&#x2D;number[0]" x="196" y="10"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[2]" x="202" y="15"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[2]/vUnCom/xsl:value&#x2D;of" x="282" y="15"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[2]/vUnCom/xsl:value&#x2D;of/format&#x2D;number[0]" x="236" y="10"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[3]" x="122" y="15"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[3]/vUnCom/xsl:value&#x2D;of" x="162" y="15"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/vProd/xsl:value&#x2D;of" x="82" y="15"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/vProd/xsl:value&#x2D;of/number[0]" x="36" y="10"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[4]/!=[0]" x="0" y="13"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[4]" x="42" y="15"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[5]/=[0]" x="236" y="230"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[5]" x="282" y="231"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/qTrib/xsl:value&#x2D;of" x="202" y="231"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/qTrib/xsl:value&#x2D;of/format&#x2D;number[0]" x="156" y="228"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/vUnTrib/xsl:value&#x2D;of" x="162" y="231"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/vUnTrib/xsl:value&#x2D;of/format&#x2D;number[0]" x="116" y="228"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[6]/!=[0]" x="36" y="230"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[6]" x="82" y="231"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[7]/!=[0]" x="0" y="230"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[7]" x="42" y="231"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[8]/!=[0]" x="236" y="261"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[8]" x="282" y="263"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[9]/!=[0]" x="115" y="81"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[9]" x="186" y="66"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[9]/DI/adi/xsl:for&#x2D;each" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[9]/DI/adi/xsl:for&#x2D;each/nAdicao/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[9]/DI/adi/xsl:for&#x2D;each/nAdicao/xsl:value&#x2D;of[1]" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/!=[0]" x="135" y="333"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]" x="181" y="335"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/!=[0]" x="245" y="323"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/!=[0]/number[0]" x="199" y="317"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if" x="291" y="325"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/cProdANP/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/CODIF/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/qTemp/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/CIDE/qBCProd/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/CIDE/vAliqProd/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/CIDE/vCIDE/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/ICMSComb/vBCICMS/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/ICMSComb/vICMS/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/ICMSComb/vBCICMSST/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/ICMSComb/vICMSST/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/ICMSInter/vBCICMSSTDest/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/ICMSInter/vICMSSTDest/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/ICMSCons/vBCICMSSTCons/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/ICMSCons/vICMSSTCons/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/prod/xsl:if[10]/xsl:if/comb/ICMSCons/UFCons/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if/ICMS00/orig/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if/ICMS00/CST/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if/ICMS00/modBC/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if/ICMS00/vBC/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if/ICMS00/pICMS/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if/ICMS00/vICMS/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[1]" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[1]/ICMS10/orig/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[1]/ICMS10/CST/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[1]/ICMS10/modBC/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[1]/ICMS10/vBC/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[1]/ICMS10/pICMS/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[1]/ICMS10/vICMS/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[1]/ICMS10/modBCST/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[1]/ICMS10/vBCST/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[1]/ICMS10/pICMSST/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[1]/ICMS10/vICMSST/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[2]" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[2]/ICMS20/orig/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[2]/ICMS20/CST/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[2]/ICMS20/modBC/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[2]/ICMS20/pRedBC/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[2]/ICMS20/vBC/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[2]/ICMS20/pICMS/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[2]/ICMS20/vICMS/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[3]" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[3]/ICMS40/orig/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[3]/ICMS40/CST/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/orig/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/CST/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if/=[0]" x="196" y="199"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if/=[0]/number[0]" x="150" y="195"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if[1]/!=[0]" x="196" y="199"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if[1]/!=[0]/number[0]" x="150" y="195"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if[1]" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if[1]/vBCST/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if[2]/=[0]" x="196" y="199"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if[2]/=[0]/number[0]" x="150" y="195"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if[2]" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if[3]/!=[0]" x="196" y="199"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if[3]/!=[0]/number[0]" x="150" y="195"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if[3]" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/ICMS/xsl:if[4]/ICMS60/xsl:if[3]/vICMSST/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if" x="48" y="35"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if/IPI/xsl:if/!=[0]" x="159" y="42"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if/IPI/xsl:if" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if/IPI/xsl:if[1]/!=[0]" x="191" y="145"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if/IPI/xsl:if[1]" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if/IPI/xsl:if[2]/!=[0]" x="203" y="172"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if/IPI/xsl:if[2]" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if/IPI/xsl:if[3]/!=[0]" x="242" y="118"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if/IPI/xsl:if[3]" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if/IPI/IPINT/CST/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[1]/and[0]" x="45" y="323"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[1]/and[0]/!=[0]" x="0" y="317"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[1]/and[0]/!=[1]" x="0" y="345"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[1]/and[0]/!=[1]/number[0]" x="0" y="339"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[1]" x="42" y="263"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/!=[0]" x="196" y="292"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]" x="242" y="294"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/and[0]" x="248" y="100"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/and[0]/and[0]" x="202" y="94"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/and[0]/and[0]/!=[0]" x="156" y="88"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/and[0]/and[0]/!=[1]" x="156" y="116"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/and[0]/and[0]/!=[1]/number[0]" x="110" y="110"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/and[0]/or[1]" x="202" y="122"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/and[0]/or[1]/=[0]" x="156" y="116"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/and[0]/or[1]/=[1]" x="156" y="144"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/PISAliq/CST/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/PISAliq/vBC/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/PISAliq/vBC/xsl:value&#x2D;of/format&#x2D;number[0]" x="205" y="199"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/PISAliq/pPIS/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/PISAliq/pPIS/xsl:value&#x2D;of/format&#x2D;number[0]" x="205" y="199"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/PISAliq/vPIS/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if/PISAliq/vPIS/xsl:value&#x2D;of/format&#x2D;number[0]" x="205" y="199"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]/and[0]" x="248" y="100"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]/and[0]/and[0]" x="202" y="94"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]/and[0]/and[0]/!=[0]" x="156" y="88"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]/and[0]/and[0]/!=[1]" x="156" y="116"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]/and[0]/and[0]/!=[1]/number[0]" x="110" y="110"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]/and[0]/=[1]" x="202" y="122"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]/PISQtde/CST/xsl:value&#x2D;of" x="294" y="102"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]/PISQtde/vBC/xsl:value&#x2D;of" x="294" y="102"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]/PISQtde/vBC/xsl:value&#x2D;of/format&#x2D;number[0]" x="248" y="96"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]/PISQtde/pPIS/xsl:value&#x2D;of" x="294" y="102"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]/PISQtde/pPIS/xsl:value&#x2D;of/format&#x2D;number[0]" x="248" y="96"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]/PISQtde/vPIS/xsl:value&#x2D;of" x="294" y="102"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[1]/PISQtde/vPIS/xsl:value&#x2D;of/format&#x2D;number[0]" x="248" y="96"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]" x="205" y="203"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]/and[0]" x="202" y="94"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]/and[0]/=[0]" x="156" y="88"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]/and[0]/!=[1]" x="156" y="116"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]/and[0]/!=[1]/number[0]" x="110" y="110"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]/or[1]" x="202" y="122"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]/or[1]/or[0]" x="156" y="116"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]/or[1]/or[0]/or[0]" x="110" y="110"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]/or[1]/or[0]/or[0]/or[0]" x="64" y="104"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]/or[1]/or[0]/or[0]/or[0]/=[0]" x="18" y="98"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]/or[1]/or[0]/or[0]/or[0]/=[1]" x="18" y="126"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]/or[1]/or[0]/or[0]/=[1]" x="64" y="132"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]/or[1]/or[0]/=[1]" x="110" y="138"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/and[0]/or[1]/=[1]" x="156" y="144"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]" x="251" y="205"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[2]/PISNT/CST/xsl:value&#x2D;of" x="294" y="102"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[3]/and[0]" x="248" y="100"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[3]/and[0]/!=[0]" x="202" y="94"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[3]/and[0]/=[1]" x="202" y="122"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[3]/and[0]/=[1]/number[0]" x="156" y="116"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[2]/PIS/xsl:if[3]" x="294" y="102"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]/and[0]" x="248" y="35"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]/and[0]/and[0]" x="202" y="29"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]/and[0]/and[0]/!=[0]" x="156" y="23"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]/and[0]/and[0]/!=[1]" x="156" y="51"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]/and[0]/and[0]/!=[1]/number[0]" x="110" y="45"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]/and[0]/=[1]" x="202" y="57"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]" x="11" y="325"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]/PISQtde/CST/xsl:value&#x2D;of" x="294" y="37"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]/PISQtde/vBC/xsl:value&#x2D;of" x="294" y="37"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]/PISQtde/vBC/xsl:value&#x2D;of/format&#x2D;number[0]" x="248" y="31"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]/PISQtde/pPIS/xsl:value&#x2D;of" x="294" y="37"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]/PISQtde/pPIS/xsl:value&#x2D;of/format&#x2D;number[0]" x="248" y="31"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]/PISQtde/vPIS/xsl:value&#x2D;of" x="294" y="37"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[3]/PISQtde/vPIS/xsl:value&#x2D;of/format&#x2D;number[0]" x="248" y="31"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[4]/!=[0]" x="248" y="35"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[4]" x="294" y="37"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/!=[0]" x="335" y="107"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]" x="294" y="37"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/and[0]" x="248" y="35"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/and[0]/and[0]" x="200" y="117"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/and[0]/and[0]/!=[0]" x="44" y="32"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/and[0]/and[0]/!=[1]" x="72" y="83"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/and[0]/and[0]/!=[1]/number[0]" x="48" y="87"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/and[0]/or[1]" x="282" y="97"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/and[0]/or[1]/=[0]" x="154" y="139"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/and[0]/or[1]/=[0]/number[0]" x="48" y="96"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/and[0]/or[1]/=[1]" x="214" y="171"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/and[0]/or[1]/=[1]/number[0]" x="51" y="138"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if" x="329" y="97"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/COFINSAliq/CST/xsl:value&#x2D;of" x="294" y="37"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/COFINSAliq/vBC/xsl:value&#x2D;of" x="320" y="5"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/COFINSAliq/vBC/xsl:value&#x2D;of/format&#x2D;number[0]" x="248" y="31"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/COFINSAliq/pCOFINS/xsl:value&#x2D;of" x="309" y="60"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/COFINSAliq/pCOFINS/xsl:value&#x2D;of/format&#x2D;number[0]" x="248" y="31"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/COFINSAliq/vCOFINS/xsl:value&#x2D;of" x="296" y="0"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if/COFINSAliq/vCOFINS/xsl:value&#x2D;of/format&#x2D;number[0]" x="248" y="31"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]/and[0]" x="248" y="35"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]/and[0]/and[0]" x="200" y="117"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]/and[0]/and[0]/!=[0]" x="43" y="62"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]/and[0]/and[0]/!=[1]" x="154" y="139"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]/and[0]/and[0]/!=[1]/number[0]" x="58" y="118"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]/and[0]/or[1]" x="200" y="145"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]/and[0]/or[1]/=[0]" x="154" y="139"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]/and[0]/or[1]/=[0]/number[0]" x="108" y="133"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]/and[0]/or[1]/=[1]" x="154" y="167"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]/and[0]/or[1]/=[1]/number[0]" x="108" y="161"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]" x="128" y="22"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]/COFINSQtde/CST/xsl:value&#x2D;of" x="292" y="125"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]/COFINSQtde/vCOFINS/xsl:value&#x2D;of" x="294" y="193"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[1]/COFINSQtde/vCOFINS/xsl:value&#x2D;of/format&#x2D;number[0]" x="220" y="194"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]" x="248" y="35"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/and[0]" x="200" y="117"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/and[0]/!=[0]" x="154" y="111"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/and[0]/!=[1]" x="154" y="139"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/and[0]/!=[1]/number[0]" x="108" y="133"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]" x="200" y="145"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]/or[0]" x="154" y="139"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]/or[0]/or[0]" x="108" y="133"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]/or[0]/or[0]/or[0]" x="62" y="127"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]/or[0]/or[0]/or[0]/=[0]" x="16" y="121"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]/or[0]/or[0]/or[0]/=[0]/number[0]" x="0" y="115"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]/or[0]/or[0]/or[0]/=[1]" x="16" y="149"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]/or[0]/or[0]/or[0]/=[1]/number[0]" x="0" y="143"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]/or[0]/or[0]/=[1]" x="62" y="155"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]/or[0]/or[0]/=[1]/number[0]" x="16" y="149"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]/or[0]/=[1]" x="108" y="161"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]/or[0]/=[1]/number[0]" x="62" y="155"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]/=[1]" x="154" y="167"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/and[0]/or[1]/=[1]/number[0]" x="108" y="161"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]" x="294" y="37"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[2]/COFINSNT/CST/xsl:value&#x2D;of" x="292" y="125"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[3]/and[0]" x="246" y="123"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[3]/and[0]/!=[0]" x="200" y="117"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[3]/and[0]/=[1]" x="200" y="145"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[3]/and[0]/=[1]/number[0]" x="154" y="139"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/imposto/xsl:if[5]/COFINS/xsl:if[3]" x="292" y="125"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/xsl:if/!=[0]" x="123" y="268"/><block path="NFe/infNFe/xsl:for&#x2D;each/det/xsl:if" x="185" y="251"/><block path="NFe/infNFe/total/ICMSTot/vBC/xsl:value&#x2D;of" x="282" y="294"/><block path="NFe/infNFe/total/ICMSTot/vBC/xsl:value&#x2D;of/format&#x2D;number[0]" x="236" y="289"/><block path="NFe/infNFe/total/ICMSTot/vICMS/xsl:value&#x2D;of" x="202" y="294"/><block path="NFe/infNFe/total/ICMSTot/vICMS/xsl:value&#x2D;of/format&#x2D;number[0]" x="156" y="289"/><block path="NFe/infNFe/total/ICMSTot/vBCST/xsl:value&#x2D;of" x="82" y="294"/><block path="NFe/infNFe/total/ICMSTot/vBCST/xsl:value&#x2D;of/format&#x2D;number[0]" x="36" y="289"/><block path="NFe/infNFe/total/ICMSTot/vST/xsl:value&#x2D;of" x="42" y="294"/><block path="NFe/infNFe/total/ICMSTot/vST/xsl:value&#x2D;of/format&#x2D;number[0]" x="0" y="289"/><block path="NFe/infNFe/total/ICMSTot/vProd/xsl:value&#x2D;of" x="242" y="324"/><block path="NFe/infNFe/total/ICMSTot/vProd/xsl:value&#x2D;of/format&#x2D;number[0]" x="196" y="320"/><block path="NFe/infNFe/total/ICMSTot/vFrete/xsl:value&#x2D;of" x="282" y="324"/><block path="NFe/infNFe/total/ICMSTot/vFrete/xsl:value&#x2D;of/format&#x2D;number[0]" x="236" y="320"/><block path="NFe/infNFe/total/ICMSTot/vSeg/xsl:value&#x2D;of" x="202" y="324"/><block path="NFe/infNFe/total/ICMSTot/vSeg/xsl:value&#x2D;of/format&#x2D;number[0]" x="156" y="320"/><block path="NFe/infNFe/total/ICMSTot/vDesc/xsl:value&#x2D;of" x="162" y="324"/><block path="NFe/infNFe/total/ICMSTot/vDesc/xsl:value&#x2D;of/format&#x2D;number[0]" x="116" y="320"/><block path="NFe/infNFe/total/ICMSTot/vII/xsl:value&#x2D;of" x="122" y="324"/><block path="NFe/infNFe/total/ICMSTot/vII/xsl:value&#x2D;of/format&#x2D;number[0]" x="76" y="320"/><block path="NFe/infNFe/total/ICMSTot/vIPI/xsl:value&#x2D;of" x="82" y="324"/><block path="NFe/infNFe/total/ICMSTot/vIPI/xsl:value&#x2D;of/format&#x2D;number[0]" x="36" y="320"/><block path="NFe/infNFe/total/ICMSTot/vPIS/xsl:value&#x2D;of" x="42" y="324"/><block path="NFe/infNFe/total/ICMSTot/vPIS/xsl:value&#x2D;of/format&#x2D;number[0]" x="0" y="320"/><block path="NFe/infNFe/total/ICMSTot/vCOFINS/xsl:value&#x2D;of" x="242" y="356"/><block path="NFe/infNFe/total/ICMSTot/vCOFINS/xsl:value&#x2D;of/format&#x2D;number[0]" x="196" y="351"/><block path="NFe/infNFe/total/ICMSTot/vOutro/xsl:value&#x2D;of" x="282" y="356"/><block path="NFe/infNFe/total/ICMSTot/vOutro/xsl:value&#x2D;of/format&#x2D;number[0]" x="236" y="351"/><block path="NFe/infNFe/total/ICMSTot/vNF/xsl:value&#x2D;of" x="202" y="356"/><block path="NFe/infNFe/total/ICMSTot/vNF/xsl:value&#x2D;of/format&#x2D;number[0]" x="156" y="351"/><block path="NFe/infNFe/total/ISSQNtot/vServ/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/total/ISSQNtot/vServ/xsl:value&#x2D;of/format&#x2D;number[0]" x="205" y="199"/><block path="NFe/infNFe/total/ISSQNtot/vBC/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/total/ISSQNtot/vBC/xsl:value&#x2D;of/format&#x2D;number[0]" x="205" y="199"/><block path="NFe/infNFe/total/ISSQNtot/vISS/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/total/ISSQNtot/vISS/xsl:value&#x2D;of/format&#x2D;number[0]" x="205" y="199"/><block path="NFe/infNFe/total/ISSQNtot/vPIS/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/total/ISSQNtot/vPIS/xsl:value&#x2D;of/format&#x2D;number[0]" x="205" y="199"/><block path="NFe/infNFe/total/ISSQNtot/vCOFINS/xsl:value&#x2D;of" x="251" y="205"/><block path="NFe/infNFe/total/ISSQNtot/vCOFINS/xsl:value&#x2D;of/format&#x2D;number[0]" x="205" y="199"/><block path="NFe/infNFe/transp/transporta/xsl:if/!=[0]" x="116" y="354"/><block path="NFe/infNFe/transp/transporta/xsl:if" x="162" y="356"/><block path="NFe/infNFe/transp/transporta/xsl:if[1]/!=[0]" x="36" y="354"/><block path="NFe/infNFe/transp/transporta/xsl:if[1]" x="82" y="356"/><block path="NFe/infNFe/transp/transporta/xsl:if[1]/CPF/xsl:value&#x2D;of" x="122" y="356"/><block path="NFe/infNFe/transp/transporta/xsl:if[2]/!=[0]" x="0" y="354"/><block path="NFe/infNFe/transp/transporta/xsl:if[2]" x="42" y="356"/><block path="NFe/infNFe/transp/transporta/xsl:if[3]/!=[0]" x="196" y="199"/><block path="NFe/infNFe/transp/transporta/xsl:if[3]" x="242" y="201"/><block path="NFe/infNFe/transp/transporta/xsl:if[4]/!=[0]" x="196" y="199"/><block path="NFe/infNFe/transp/transporta/xsl:if[4]" x="242" y="201"/><block path="NFe/infNFe/transp/xsl:if/!=[0]" x="76" y="138"/><block path="NFe/infNFe/transp/xsl:if/!=[0]/number[0]" x="30" y="133"/><block path="NFe/infNFe/transp/xsl:if" x="122" y="139"/><block path="NFe/infNFe/transp/veicTransp/placa/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/transp/veicTransp/xsl:if/!=[0]" x="196" y="199"/><block path="NFe/infNFe/transp/veicTransp/xsl:if" x="242" y="201"/><block path="NFe/infNFe/transp/xsl:if[1]/!=[0]" x="36" y="138"/><block path="NFe/infNFe/transp/xsl:if[1]" x="82" y="139"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each" x="42" y="139"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if/!=[0]" x="196" y="199"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if" x="242" y="201"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if[1]/!=[0]" x="196" y="199"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if[1]" x="242" y="201"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if[2]/!=[0]" x="196" y="199"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if[2]" x="242" y="201"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if[3]/!=[0]" x="196" y="199"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if[3]" x="242" y="201"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if[4]/!=[0]" x="196" y="199"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if[4]" x="242" y="201"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if[5]/!=[0]" x="196" y="199"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if[5]" x="242" y="201"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if[6]/!=[0]" x="196" y="199"/><block path="NFe/infNFe/transp/xsl:for&#x2D;each/vol/xsl:if[6]" x="242" y="201"/><block path="NFe/infNFe/cobr/xsl:if/!=[0]" x="236" y="106"/><block path="NFe/infNFe/cobr/xsl:if" x="282" y="108"/><block path="NFe/infNFe/cobr/xsl:if/fat/xsl:if/!=[0]" x="196" y="199"/><block path="NFe/infNFe/cobr/xsl:if/fat/xsl:if" x="242" y="201"/><block path="NFe/infNFe/cobr/xsl:if/fat/xsl:if/vOrig/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/cobr/xsl:if/fat/xsl:if/vOrig/xsl:value&#x2D;of/format&#x2D;number[0]" x="196" y="196"/><block path="NFe/infNFe/cobr/xsl:if/fat/xsl:if[1]/!=[0]" x="196" y="199"/><block path="NFe/infNFe/cobr/xsl:if/fat/xsl:if[1]" x="242" y="201"/><block path="NFe/infNFe/cobr/xsl:if/fat/xsl:if[1]/vDesc/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/cobr/xsl:if/fat/xsl:if[1]/vDesc/xsl:value&#x2D;of/format&#x2D;number[0]" x="196" y="196"/><block path="NFe/infNFe/cobr/xsl:if/fat/vLiq/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/cobr/xsl:if/fat/vLiq/xsl:value&#x2D;of/format&#x2D;number[0]" x="196" y="196"/><block path="NFe/infNFe/cobr/xsl:for&#x2D;each" x="132" y="84"/><block path="NFe/infNFe/cobr/xsl:for&#x2D;each/xsl:if/!=[0]" x="156" y="106"/><block path="NFe/infNFe/cobr/xsl:for&#x2D;each/xsl:if" x="202" y="108"/><block path="NFe/infNFe/cobr/xsl:for&#x2D;each/xsl:if/dup/vDup/xsl:value&#x2D;of" x="242" y="201"/><block path="NFe/infNFe/cobr/xsl:for&#x2D;each/xsl:if/dup/vDup/xsl:value&#x2D;of/format&#x2D;number[0]" x="196" y="196"/><block path="NFe/infNFe/xsl:if[2]/or[0]" x="116" y="199"/><block path="NFe/infNFe/xsl:if[2]/or[0]/!=[0]" x="70" y="195"/><block path="NFe/infNFe/xsl:if[2]/or[0]/!=[1]" x="70" y="217"/><block path="NFe/infNFe/xsl:if[2]" x="162" y="201"/><block path="NFe/infNFe/xsl:if[2]/infAdic/xsl:if/!=[0]" x="36" y="106"/><block path="NFe/infNFe/xsl:if[2]/infAdic/xsl:if" x="82" y="108"/><block path="NFe/infNFe/xsl:if[2]/infAdic/xsl:if[1]/!=[0]" x="0" y="106"/><block path="NFe/infNFe/xsl:if[2]/infAdic/xsl:if[1]" x="42" y="108"/></template></MapperBlockPosition><TemplateContext></TemplateContext><MapperFilter side="source"><Fragment url="" path="" action="hideAll"/><Fragment url="file:///c:/Projetos/Fisconet4/XML Canonico 1.10 2.xml" path="/NFe/infNFe/det/imposto/COFINS" action="showFragment"/></MapperFilter></MapperMetaTag>
</metaInformation>
-->