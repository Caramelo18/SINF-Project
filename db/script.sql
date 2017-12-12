IF OBJECT_ID('dbo.AccountReceivable', 'U') IS NOT NULL
  DROP TABLE [dbo].[AccountReceivable];

IF OBJECT_ID('dbo.AccountPayable', 'U') IS NOT NULL
  DROP TABLE [dbo].[AccountPayable];

IF OBJECT_ID('dbo.Company', 'U') IS NOT NULL
  DROP TABLE [dbo].[Company];

IF OBJECT_ID('dbo.Line', 'U') IS NOT NULL
  DROP TABLE [dbo].[Line];

IF OBJECT_ID('dbo.Invoice', 'U') IS NOT NULL
  DROP TABLE [dbo].[Invoice];

IF OBJECT_ID('dbo.DocumentTotals', 'U') IS NOT NULL
  DROP TABLE [dbo].[DocumentTotals];

IF OBJECT_ID('dbo.OrderReferences', 'U') IS NOT NULL
  DROP TABLE [dbo].[OrderReferences];

IF OBJECT_ID('dbo.SalesInvoices', 'U') IS NOT NULL
  DROP TABLE [dbo].[SalesInvoices];

IF OBJECT_ID('dbo.Customer', 'U') IS NOT NULL
  DROP TABLE [dbo].[Customer];

IF OBJECT_ID('dbo.BillingAddress', 'U') IS NOT NULL
  DROP TABLE [dbo].[BillingAddress];

IF OBJECT_ID('dbo.LinhaDocCompra', 'U') IS NOT NULL
  DROP TABLE [dbo].[LinhaDocCompra];

IF OBJECT_ID('dbo.DocCompra', 'U') IS NOT NULL
  DROP TABLE [dbo].[DocCompra];

IF OBJECT_ID('dbo.LinhaDocVenda', 'U') IS NOT NULL
  DROP TABLE [dbo].[LinhaDocVenda];

IF OBJECT_ID('dbo.DocVenda', 'U') IS NOT NULL
  DROP TABLE [dbo].[DocVenda];

IF OBJECT_ID('dbo.Product', 'U') IS NOT NULL
  DROP TABLE [dbo].[Product];

IF OBJECT_ID('dbo.Supplier', 'U') IS NOT NULL
  DROP TABLE [dbo].[Supplier];

IF OBJECT_ID('dbo.Tax', 'U') IS NOT NULL
  DROP TABLE [dbo].[Tax];

/****** Object:  Table [dbo].[AccountReceivable]    Script Date: 04-12-2017 15:48:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccountReceivable](
	[Entidade] [varchar](50) NOT NULL,
	[DataDoc] [datetime] NOT NULL,
	[DataVenc] [datetime] NOT NULL,
	[ValorTotal] [float] NOT NULL,
	[ValorPendente] [float] NOT NULL,
	[ModoPag] [varchar](50) NULL,
	[TipoEntidade] [varchar](50) NULL,
 CONSTRAINT [PK_AccountReceivable] PRIMARY KEY CLUSTERED
(
	[Entidade] ASC,
	[DataDoc] ASC,
	[DataVenc] ASC,
	[ValorTotal] ASC,
	[ValorPendente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AccountPayable]    Script Date: 04-12-2017 19:18:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccountPayable](
	[Entidade] [varchar](50) NOT NULL,
	[DataDoc] [datetime] NOT NULL,
	[DataVenc] [datetime] NOT NULL,
	[ValorTotal] [float] NOT NULL,
	[ValorPendente] [float] NOT NULL,
	[ModoPag] [varchar](50) NULL,
	[TipoEntidade] [varchar](50) NULL,
 CONSTRAINT [PK_AccountPayable] PRIMARY KEY CLUSTERED
(
	[Entidade] ASC,
	[DataDoc] ASC,
	[DataVenc] ASC,
	[ValorTotal] ASC,
	[ValorPendente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO


/****** Object:  Table [dbo].[BillingAddress]    Script Date: 04-12-2017 19:18:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BillingAddress](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AddressDetail] [varchar](200) NULL,
	[City] [varchar](200) NULL,
	[PostalCode] [varchar](20) NULL,
	[Country] [varchar](20) NULL,
PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Company]    Script Date: 04-12-2017 19:18:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyID] [varchar](50) NOT NULL,
	[TaxRegistrationNumber] [varbinary](50) NOT NULL,
	[CompanyName] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 04-12-2017 19:18:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [varchar](50) NOT NULL,
	[AccountID] [varchar](50) NULL,
	[CustomerTaxID] [varchar](50) NULL,
	[CompanyName] [varchar](200) NULL,
	[BillingAddressID] [int] NULL,
	[Telephone] [varchar](20) NULL,
	[SelfBillingIndicator] [int] NOT NULL DEFAULT ((0)),
	[CustomerName] [varchar](50) NULL,
	[Currency] [varchar](10) NULL,
	[CustomerEmail] [varchar](250) NULL,
	[Fax] [varchar](50) NULL,
	[Website] [varchar](50) NULL,
	[NumberPurchases] [int] NULL,
PRIMARY KEY CLUSTERED
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DocCompra]    Script Date: 04-12-2017 19:18:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DocCompra](
	[Id] [varchar](50) NOT NULL,
	[NumDocExterno] [varchar](50) NULL,
	[Entidade] [varchar](50) NULL,
	[NumDoc] [int] NULL,
	[Data] [datetime] NULL,
	[TotalMerc] [float] NULL,
	[Serie] [varchar](50) NULL,
 CONSTRAINT [PK_DocCompra] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LinhaDocCompra]    Script Date: 04-12-2017 19:18:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LinhaDocCompra](
	[CodArtigo] [varchar](50) NULL,
	[DescArtigo] [varchar](50) NULL,
	[IdCabecDoc] [varchar](50) NOT NULL,
	[NumLinha] [int] NULL,
	[Quantidade] [float] NULL,
	[Unidade] [varchar](50) NULL,
	[Desconto] [float] NULL,
	[PrecoUnitario] [float] NULL,
	[TotalILiquido] [float] NULL,
	[TotalLiquido] [float] NULL,
	[Armazem] [varchar](50) NULL,
	[Lote] [varchar](50) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_LinhaDocCompra] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DocVenda]    Script Date: 04-12-2017 19:18:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DocVenda](
	[Id] [varchar](50) NOT NULL,
	[Entidade] [varchar](50) NULL,
	[NumDoc] [int] NULL,
	[Data] [datetime] NULL,
	[TotalMerc] [float] NULL,
	[Serie] [varchar](50) NULL,
 CONSTRAINT [PK_DocVenda] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LinhaDocVenda]    Script Date: 04-12-2017 19:18:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LinhaDocVenda](
	[CodArtigo] [varchar](50) NULL,
	[DescArtigo] [varchar](50) NULL,
	[IdCabecDoc] [varchar](50) NOT NULL,
	[Quantidade] [float] NULL,
	[Unidade] [varchar](50) NULL,
	[Desconto] [float] NULL,
	[PrecoUnitario] [float] NULL,
	[TotalILiquido] [float] NULL,
	[TotalLiquido] [float] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_LinhaDocVenda] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 04-12-2017 19:18:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[ProductType] [varchar](10) NULL,
	[ProductCode] [varchar](50) NOT NULL,
	[ProductGroup] [varchar](50) NULL,
	[ProductDescription] [varchar](200) NULL,
	[ProductNumberCode] [varchar](50) NULL,
	[ProductStock] [int] NOT NULL DEFAULT ((0)),
	[Armazem] [varchar](100) NULL,
PRIMARY KEY CLUSTERED
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[DocumentTotals]    Script Date: 13-11-2017 15:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DocumentTotals](
	[InvoiceNo] [varchar](50) NOT NULL,
	[TaxPayable] [float] NOT NULL,
	[NetTotal] [float] NOT NULL,
	[GrossTotal] [float] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[InvoiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Invoice]    Script Date: 13-11-2017 15:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceNo] [varchar](50) NOT NULL,
	[InvoiceStatus] [varchar](10) NULL,
	[Hash] [varchar](200) NULL,
	[HashControl] [varchar](10) NULL,
	[Period] [varchar](10) NULL,
	[InvoiceDate] [varchar](50) NULL,
	[InvoiceType] [varchar](10) NULL,
	[SelfBillingIndicator] [varchar](10) NULL,
	[SystemEntryDate] [varchar](50) NULL,
	[CustomerID] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED
(
	[InvoiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Line]    Script Date: 13-11-2017 15:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Line](
	[LineNumber] [varchar](50) NOT NULL,
	[InvoiceNo] [varchar](50) NOT NULL,
	[ProductCode] [varchar](50) NOT NULL,
	[ProductDescription] [varchar](200) NULL,
	[Quantity] [varchar](50) NOT NULL,
	[UnitOfMeasure] [varchar](10) NOT NULL,
	[UnitPrice] [varchar](50) NOT NULL,
	[TaxPointDate] [varchar](50) NULL,
	[Description] [varchar](200) NULL,
	[CreditAmount] [varchar](50) NULL,
	[SettlementAmount] [varchar](50) NULL,
	[TaxExemptionReason] [varchar](200) NULL,
 CONSTRAINT [PK_Line] PRIMARY KEY NONCLUSTERED
(
	[LineNumber] ASC,
	[InvoiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[OrderReferences]    Script Date: 13-11-2017 15:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrderReferences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OriginatingON] [varchar](50) NULL,
	[OrderDate] [varchar](50) NULL,
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[SalesInvoices]    Script Date: 13-11-2017 15:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesInvoices](
	[Id] [int] NOT NULL,
	[NumberOfEntries] [int] NOT NULL DEFAULT ((0)),
	[TotalDebit] [float] NOT NULL DEFAULT ((0)),
	[TotalCredit] [float] NOT NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[Supplier]    Script Date: 04-12-2017 19:18:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supplier](
	[CodFornecedor] [varchar](50) NOT NULL,
	[Nome] [varchar](50) NULL,
	[NomeFiscal] [varchar](100) NULL,
	[Telefone] [varchar](50) NULL,
	[Contribuinte] [varchar](50) NULL,
PRIMARY KEY CLUSTERED
(
	[CodFornecedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Tax]    Script Date: 13-11-2017 15:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tax](
	[LineNumber] [varchar](50) NOT NULL,
	[InvoiceNo] [varchar](50) NOT NULL,
	[TaxType] [varchar](10) NULL,
	[TaxCountryRegion] [varchar](50) NULL,
	[TaxCode] [varchar](10) NULL,
	[TaxPercentage] [varchar](50) NULL,
	[SettlementAmount] [varchar](50) NULL,
 CONSTRAINT [PK_Tax] PRIMARY KEY NONCLUSTERED
(
	[LineNumber] ASC,
	[InvoiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_BillingAddress] FOREIGN KEY([BillingAddressID])
REFERENCES [dbo].[BillingAddress] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_BillingAddress]
GO
ALTER TABLE [dbo].[LinhaDocCompra]  WITH CHECK ADD  CONSTRAINT [FK_DocCompra_LinhaDocCompra] FOREIGN KEY([IdCabecDoc])
REFERENCES [dbo].[DocCompra] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LinhaDocCompra] CHECK CONSTRAINT [FK_DocCompra_LinhaDocCompra]
GO
ALTER TABLE [dbo].[LinhaDocVenda]  WITH CHECK ADD  CONSTRAINT [FK_DocVenda_LinhaDocVenda] FOREIGN KEY([IdCabecDoc])
REFERENCES [dbo].[DocVenda] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LinhaDocCompra] CHECK CONSTRAINT [FK_DocCompra_LinhaDocCompra]

