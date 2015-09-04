<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:my2c="http://www.my2cents.co.nz">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>

  <!-- Function to convert HTCs HH:MM:SS time format to number of seconds. -->
  <xsl:function name="my2c:convertToSeconds">
    <xsl:param name="time" as="xs:string"/>
    <xsl:value-of select="(( substring-before($time, ':')                      cast as xs:integer ) * 60 * 60 ) + 
                          (( substring-before(substring-after($time,':'), ':') cast as xs:integer ) * 60      ) + 
                          (  substring-after (substring-after($time,':'), ':') cast as xs:integer )" 
    />
  </xsl:function>

  <xsl:template match="/">
		<AcesHighPilotScore xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
			<GameId><xsl:value-of select='substring-after("/html/body/table/tr/td/font/b", "Scores for ")'/></GameId>
			<TourId></TourId>
			<TourDetails></TourDetails>
			<TourType></TourType>
			<VsEnemy>
				<Fighter>
					<KillToDeathPlus1>
            <Score><xsl:value-of select='normalize-space(/html/body/table[2]/tr[3]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[2]/tr[3]/td[3])'/></Rank> 
					</KillToDeathPlus1>
					<KillPerSortie>
						<Score><xsl:value-of select='normalize-space(/html/body/table[2]/tr[4]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[2]/tr[4]/td[3])'/></Rank>
					</KillPerSortie>
					<KillPerHour>
						<Score><xsl:value-of select='normalize-space(/html/body/table[2]/tr[5]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[2]/tr[5]/td[3])'/></Rank>
					</KillPerHour>
					<HitPercentage>
						<Score><xsl:value-of select='normalize-space(/html/body/table[2]/tr[6]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[2]/tr[6]/td[3])'/></Rank>
					</HitPercentage>
					<Points>
						<Score><xsl:value-of select='normalize-space(/html/body/table[2]/tr[7]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[2]/tr[7]/td[3])'/></Rank>
					</Points>
				</Fighter>
				<Attack>
					<KillToDeathPlus1>
            <Score><xsl:value-of select='normalize-space(/html/body/table[4]/tr[3]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[4]/tr[3]/td[3])'/></Rank> 
					</KillToDeathPlus1>
					<KillPerSortie>
						<Score><xsl:value-of select='normalize-space(/html/body/table[4]/tr[4]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[4]/tr[4]/td[3])'/></Rank>
					</KillPerSortie>
					<KillPerHour>
						<Score><xsl:value-of select='normalize-space(/html/body/table[4]/tr[5]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[4]/tr[5]/td[3])'/></Rank>
					</KillPerHour>
					<HitPercentage>
						<Score><xsl:value-of select='normalize-space(/html/body/table[4]/tr[6]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[4]/tr[6]/td[3])'/></Rank>
					</HitPercentage>
					<Points>
						<Score><xsl:value-of select='normalize-space(/html/body/table[4]/tr[7]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[4]/tr[7]/td[3])'/></Rank>
					</Points>
				</Attack>
				<VehicleBoat>
					<KillToDeathPlus1>
						<Score><xsl:value-of select='normalize-space(/html/body/table[5]/tr[3]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[5]/tr[3]/td[3])'/></Rank>
					</KillToDeathPlus1>
					<KillPerSortie>
						<Score><xsl:value-of select='normalize-space(/html/body/table[5]/tr[4]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[5]/tr[4]/td[3])'/></Rank>
					</KillPerSortie>
					<KillPerHour>
						<Score><xsl:value-of select='normalize-space(/html/body/table[5]/tr[5]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[5]/tr[5]/td[3])'/></Rank>
					</KillPerHour>
					<HitPercentage>
						<Score><xsl:value-of select='normalize-space(/html/body/table[5]/tr[6]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[5]/tr[6]/td[3])'/></Rank>
					</HitPercentage>
					<Points>
						<Score><xsl:value-of select='normalize-space(/html/body/table[5]/tr[7]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[5]/tr[7]/td[3])'/></Rank>
					</Points>
				</VehicleBoat>
			</VsEnemy>
			<VsObjects>
				<Bomber>
					<DamagePerDeathPlus1>
						<Score><xsl:value-of select='normalize-space(/html/body/table[3]/tr[3]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[3]/tr[3]/td[3])'/></Rank>
					</DamagePerDeathPlus1>
					<DamagePerSortie>
						<Score><xsl:value-of select='normalize-space(/html/body/table[3]/tr[4]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[3]/tr[4]/td[3])'/></Rank>
					</DamagePerSortie>
					<HitPercentage>
						<Score><xsl:value-of select='normalize-space(/html/body/table[3]/tr[5]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[3]/tr[5]/td[3])'/></Rank>
					</HitPercentage>
					<Points>
						<Score><xsl:value-of select='normalize-space(/html/body/table[3]/tr[6]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[3]/tr[6]/td[3])'/></Rank>
					</Points>
					<FieldCaptures>
						<Score><xsl:value-of select='normalize-space(/html/body/table[3]/tr[7]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[3]/tr[7]/td[3])'/></Rank>
					</FieldCaptures>
				</Bomber>
				<Attack>
					<DamagePerDeathPlus1>
						<Score><xsl:value-of select='normalize-space(/html/body/table[4]/tr[8]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[4]/tr[8]/td[3])'/></Rank>
					</DamagePerDeathPlus1>
					<DamagePerSortie>
						<Score><xsl:value-of select='normalize-space(/html/body/table[4]/tr[9]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[4]/tr[9]/td[3])'/></Rank>
					</DamagePerSortie>
					<HitPercentage>
						<Score><xsl:value-of select='normalize-space(/html/body/table[4]/tr[10]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[4]/tr[10]/td[3])'/></Rank>
					</HitPercentage>
					<Points>
						<Score><xsl:value-of select='normalize-space(/html/body/table[4]/tr[11]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[4]/tr[11]/td[3])'/></Rank>
					</Points>
					<FieldCaptures>
						<Score><xsl:value-of select='normalize-space(/html/body/table[4]/tr[12]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[4]/tr[12]/td[3])'/></Rank>
					</FieldCaptures>
				</Attack>
				<VehicleBoat>
					<DamagePerDeathPlus1>
						<Score><xsl:value-of select='normalize-space(/html/body/table[5]/tr[8]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[5]/tr[8]/td[3])'/></Rank>
					</DamagePerDeathPlus1>
					<DamagePerSortie>
						<Score><xsl:value-of select='normalize-space(/html/body/table[5]/tr[9]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[5]/tr[9]/td[3])'/></Rank>
					</DamagePerSortie>
					<HitPercentage>
						<Score><xsl:value-of select='normalize-space(/html/body/table[5]/tr[10]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[5]/tr[10]/td[3])'/></Rank>
					</HitPercentage>
					<Points>
						<Score><xsl:value-of select='normalize-space(/html/body/table[5]/tr[11]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[5]/tr[11]/td[3])'/></Rank>
					</Points>
					<FieldCaptures>
						<Score><xsl:value-of select='normalize-space(/html/body/table[5]/tr[12]/td[2])'/></Score>
						<Rank><xsl:value-of select='normalize-space(/html/body/table[5]/tr[12]/td[3])'/></Rank>
					</FieldCaptures>
				</VehicleBoat>
			</VsObjects>
			<Overall>
				<Fighter>
					<Kills><xsl:value-of select='normalize-space(/html/body/table[6]/td[2])'/></Kills>
					<Assists><xsl:value-of select='normalize-space(/html/body/table[6]/td[8])'/></Assists>
					<Sorties><xsl:value-of select='normalize-space(/html/body/table[6]/td[14])'/></Sorties>
					<Landed><xsl:value-of select='normalize-space(/html/body/table[6]/td[20])'/></Landed>
					<Bailed><xsl:value-of select='normalize-space(/html/body/table[6]/td[26])'/></Bailed>
					<Ditched><xsl:value-of select='normalize-space(/html/body/table[6]/td[32])'/></Ditched>
					<Captured><xsl:value-of select='normalize-space(/html/body/table[6]/td[38])'/></Captured>
					<Death><xsl:value-of select='normalize-space(/html/body/table[6]/td[44])'/></Death>
					<Disco><xsl:value-of select='normalize-space(/html/body/table[6]/td[50])'/></Disco>
					<Time><xsl:value-of select='my2c:convertToSeconds(normalize-space(/html/body/table[6]/td[56]))'/></Time>
					<Rank><xsl:value-of select='normalize-space(/html/body/table[6]/td[62])'/></Rank>
				</Fighter>
				<Bomber>
					<Kills><xsl:value-of select='normalize-space(/html/body/table[6]/td[3])'/></Kills>
					<Assists><xsl:value-of select='normalize-space(/html/body/table[6]/td[9])'/></Assists>
					<Sorties><xsl:value-of select='normalize-space(/html/body/table[6]/td[15])'/></Sorties>
					<Landed><xsl:value-of select='normalize-space(/html/body/table[6]/td[21])'/></Landed>
					<Bailed><xsl:value-of select='normalize-space(/html/body/table[6]/td[27])'/></Bailed>
					<Ditched><xsl:value-of select='normalize-space(/html/body/table[6]/td[33])'/></Ditched>
					<Captured><xsl:value-of select='normalize-space(/html/body/table[6]/td[39])'/></Captured>
					<Death><xsl:value-of select='normalize-space(/html/body/table[6]/td[45])'/></Death>
					<Disco><xsl:value-of select='normalize-space(/html/body/table[6]/td[51])'/></Disco>
					<Time><xsl:value-of select='my2c:convertToSeconds(normalize-space(/html/body/table[6]/td[57]))'/></Time>
					<Rank><xsl:value-of select='normalize-space(/html/body/table[6]/td[63])'/></Rank>
				</Bomber>
				<Attack>
					<Kills><xsl:value-of select='normalize-space(/html/body/table[6]/td[4])'/></Kills>
					<Assists><xsl:value-of select='normalize-space(/html/body/table[6]/td[10])'/></Assists>
					<Sorties><xsl:value-of select='normalize-space(/html/body/table[6]/td[16])'/></Sorties>
					<Landed><xsl:value-of select='normalize-space(/html/body/table[6]/td[22])'/></Landed>
					<Bailed><xsl:value-of select='normalize-space(/html/body/table[6]/td[28])'/></Bailed>
					<Ditched><xsl:value-of select='normalize-space(/html/body/table[6]/td[34])'/></Ditched>
					<Captured><xsl:value-of select='normalize-space(/html/body/table[6]/td[40])'/></Captured>
					<Death><xsl:value-of select='normalize-space(/html/body/table[6]/td[46])'/></Death>
					<Disco><xsl:value-of select='normalize-space(/html/body/table[6]/td[52])'/></Disco>
					<Time><xsl:value-of select='my2c:convertToSeconds(normalize-space(/html/body/table[6]/td[58]))'/></Time>
					<Rank><xsl:value-of select='normalize-space(/html/body/table[6]/td[64])'/></Rank>
				</Attack>
				<VehicleBoat>
					<Kills><xsl:value-of select='normalize-space(/html/body/table[6]/td[5])'/></Kills>
					<Assists><xsl:value-of select='normalize-space(/html/body/table[6]/td[11])'/></Assists>
					<Sorties><xsl:value-of select='normalize-space(/html/body/table[6]/td[17])'/></Sorties>
					<Landed><xsl:value-of select='normalize-space(/html/body/table[6]/td[23])'/></Landed>
					<Bailed><xsl:value-of select='normalize-space(/html/body/table[6]/td[29])'/></Bailed>
					<Ditched><xsl:value-of select='normalize-space(/html/body/table[6]/td[35])'/></Ditched>
					<Captured><xsl:value-of select='normalize-space(/html/body/table[6]/td[41])'/></Captured>
					<Death><xsl:value-of select='normalize-space(/html/body/table[6]/td[47])'/></Death>
					<Disco><xsl:value-of select='normalize-space(/html/body/table[6]/td[53])'/></Disco>
					<Time><xsl:value-of select='my2c:convertToSeconds(normalize-space(/html/body/table[6]/td[59]))'/></Time>
					<Rank><xsl:value-of select='normalize-space(/html/body/table[6]/td[65])'/></Rank>
				</VehicleBoat>
				<Total>
					<Kills><xsl:value-of select='normalize-space(/html/body/table[6]/td[6])'/></Kills>
					<Assists><xsl:value-of select='normalize-space(/html/body/table[6]/td[12])'/></Assists>
					<Sorties><xsl:value-of select='normalize-space(/html/body/table[6]/td[18])'/></Sorties>
					<Landed><xsl:value-of select='normalize-space(/html/body/table[6]/td[24])'/></Landed>
					<Bailed><xsl:value-of select='normalize-space(/html/body/table[6]/td[30])'/></Bailed>
					<Ditched><xsl:value-of select='normalize-space(/html/body/table[6]/td[36])'/></Ditched>
					<Captured><xsl:value-of select='normalize-space(/html/body/table[6]/td[42])'/></Captured>
					<Death><xsl:value-of select='normalize-space(/html/body/table[6]/td[48])'/></Death>
					<Disco><xsl:value-of select='normalize-space(/html/body/table[6]/td[54])'/></Disco>
					<Time><xsl:value-of select='my2c:convertToSeconds(normalize-space(/html/body/table[6]/td[60]))'/></Time>
					<Rank><xsl:value-of select='normalize-space(/html/body/table[6]/td[66])'/></Rank>
				</Total>
			</Overall>
		</AcesHighPilotScore>
	</xsl:template>
</xsl:stylesheet>
