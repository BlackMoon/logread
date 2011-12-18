<?xml version="1.0" encoding="UTF-8" ?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">

    <html>

      <title>

        <!-- Укажите заголовок отчета -->

      </title>

      <body style="font-family:Tahoma;font-size:12pt">

        <!-- Укажите содержимое отчета -->

        <!-- Вставка параметра отчета: <xsl:value-of select="/отчет/параметры/[ИмяПараметра]" /> -->

        <!-- Вставка табличной части: <xsl:apply-templates select="/отчет/секции/[ИмяСекции]" /> -->

      </body>

    </html>

  </xsl:template>


  <xsl:template match="/отчет/секции">

    <!--Укажите шаблон для табличной части-->

  </xsl:template>

</xsl:stylesheet>
