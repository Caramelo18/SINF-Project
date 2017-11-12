GO
/****** Object:  Table [dbo].[AllSalesInvoicesInfo]    Script Date: 12-11-2017 01:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllSalesInvoicesInfo](
	[Id] [int] NOT NULL,
	[NumberOfEntries] [int] NULL,
	[TotalDebit] [float] NULL,
	[TotalCredit] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BillingAddress]    Script Date: 12-11-2017 01:43:57 ******/
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
	[Country] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Company]    Script Date: 12-11-2017 01:43:57 ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 12-11-2017 01:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [varchar](50) NOT NULL,
	[AccountID] [varchar](50) NOT NULL,
	[CustomerTaxID] [varchar](50) NOT NULL,
	[CompanyName] [varchar](200) NULL,
	[BillingAddressID] [int] NULL,
	[Telephone] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12-11-2017 01:43:57 ******/
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
	[ProductStock] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SaleInvoice]    Script Date: 12-11-2017 01:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SaleInvoice](
	[InvoiceNo] [varchar](50) NOT NULL,
	[InvoiceStatus] [varchar](10) NULL,
	[Period] [varchar](10) NULL,
	[InvoiceDate] [date] NULL,
	[SystemEntryDate] [datetime] NULL,
	[CustomerID] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SaleInvoiceLine]    Script Date: 12-11-2017 01:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SaleInvoiceLine](
	[LineNumber] [int] NOT NULL,
	[SaleInvoiceNo] [varchar](50) NOT NULL,
	[ProductCode] [varchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitOfMeasure] [varchar](10) NOT NULL,
	[UnitPrice] [float] NOT NULL,
	[TaxPointDate] [date] NULL,
	[CreditAmount] [float] NULL,
 CONSTRAINT [PK_SalesInvoiceLine] PRIMARY KEY NONCLUSTERED 
(
	[LineNumber] ASC,
	[SaleInvoiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SaleInvoiceTotal]    Script Date: 12-11-2017 01:43:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SaleInvoiceTotal](
	[SaleInvoiceNo] [varchar](50) NOT NULL,
	[TaxPayable] [float] NOT NULL,
	[NetTotal] [float] NOT NULL,
	[GrossTotal] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SaleInvoiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_BillingAddress] FOREIGN KEY([BillingAddressID])
REFERENCES [dbo].[BillingAddress] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_BillingAddress]
GO
ALTER TABLE [dbo].[SaleInvoice]  WITH CHECK ADD  CONSTRAINT [FK_SaleInvoice_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SaleInvoice] CHECK CONSTRAINT [FK_SaleInvoice_Customer]
GO
ALTER TABLE [dbo].[SaleInvoiceLine]  WITH CHECK ADD  CONSTRAINT [FK_SaleInvoiceLine_Product] FOREIGN KEY([ProductCode])
REFERENCES [dbo].[Product] ([ProductCode])
GO
ALTER TABLE [dbo].[SaleInvoiceLine] CHECK CONSTRAINT [FK_SaleInvoiceLine_Product]
GO
ALTER TABLE [dbo].[SaleInvoiceLine]  WITH CHECK ADD  CONSTRAINT [FK_SaleInvoiceLine_SaleInvoice] FOREIGN KEY([SaleInvoiceNo])
REFERENCES [dbo].[SaleInvoice] ([InvoiceNo])
GO
ALTER TABLE [dbo].[SaleInvoiceLine] CHECK CONSTRAINT [FK_SaleInvoiceLine_SaleInvoice]
GO
ALTER TABLE [dbo].[SaleInvoiceTotal]  WITH CHECK ADD  CONSTRAINT [FK_SaleInvoiceTotal_SaleInvoice] FOREIGN KEY([SaleInvoiceNo])
REFERENCES [dbo].[SaleInvoice] ([InvoiceNo])
GO
ALTER TABLE [dbo].[SaleInvoiceTotal] CHECK CONSTRAINT [FK_SaleInvoiceTotal_SaleInvoice]
GO
