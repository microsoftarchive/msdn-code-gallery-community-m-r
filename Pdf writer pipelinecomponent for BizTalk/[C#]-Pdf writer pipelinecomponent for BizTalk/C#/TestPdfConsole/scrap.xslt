<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>

  <xsl:template name="table-header-cell">
    <xsl:param name="name"/>
    <xsl:element name="fo:table-cell">
      <xsl:attribute name="border-style">solid</xsl:attribute>
      <xsl:element name="fo:block">
        <xsl:attribute name="font-weight">bold</xsl:attribute>
        <xsl:value-of select="$name"/>
      </xsl:element>
    </xsl:element>
  </xsl:template>
  
</xsl:stylesheet>
