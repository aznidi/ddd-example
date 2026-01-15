using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SMS.Application.Common.Services;
using SMS.Domain.Modules.Finance.Aggregates;
using SMS.Domain.Modules.Finance.Entities;
using SMS.Domain.Modules.Finance.ValueObjects;

namespace SMS.Infrastructure.Services;

public sealed class EngagementPdfService : IEngagementPdfService
{
    public EngagementPdfService()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public byte[] GenerateEngagementContract(Engagement engagement, Student student)
    {
        // Palette (simple & cohérente)
        var primary = Colors.Blue.Darken2;
        var text = Colors.Grey.Darken4;
        var border = Colors.Grey.Lighten2;
        var zebra = Colors.Grey.Lighten4;
        var soft = Colors.Grey.Lighten5;

        return Document.Create(doc =>
        {
            doc.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);

                page.DefaultTextStyle(x =>
                    x.FontFamily("Helvetica")
                     .FontSize(10)
                     .FontColor(text));

                page.Header().Element(header =>
                {
                    header.Column(col =>
                    {
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().AlignCenter().Text("CONTRAT D'ENGAGEMENT SCOLAIRE")
                                .FontSize(18).SemiBold().FontColor(primary);

                            // (Optionnel) si tu veux un “badge” date sans ajouter de données métier
                            row.ConstantItem(140).AlignRight().Text($"Généré le {DateTime.UtcNow:dd/MM/yyyy}")
                                .FontSize(9).FontColor(Colors.Grey.Darken1);
                        });

                        col.Item().PaddingTop(6).LineHorizontal(1).LineColor(border);
                    });
                });

                page.Content().PaddingTop(16).Column(column =>
                {
                    column.Spacing(16);

                    column.Item().Element(card =>
                    {
                        card.Border(1).BorderColor(border)
                            .Background(soft)
                            .Padding(12)
                            .Row(row =>
                            {
                                row.RelativeItem().Column(c =>
                                {
                                    c.Item().Text("Informations élève").SemiBold().FontSize(11).FontColor(primary);

                                    c.Item().PaddingTop(6).Row(r =>
                                    {
                                        r.ConstantItem(120).Text("Élève :").SemiBold();
                                        r.RelativeItem().Text(student.GetFullName());
                                    });

                                    c.Item().Row(r =>
                                    {
                                        r.ConstantItem(120).Text("Date de naissance :").SemiBold();
                                        r.RelativeItem().Text(student.BirthDate.Value.ToString("dd/MM/yyyy"));
                                    });
                                });
                            });
                    });

                    column.Item().Element(section =>
                    {
                        section.Column(col =>
                        {
                            col.Item().Element(c => { SectionTitle(c, "SERVICES SOUSCRITS", primary); });

                            col.Item().PaddingTop(8).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3); // Service
                                    columns.RelativeColumn(1); // Prix
                                    columns.RelativeColumn(1); // Quantité
                                    columns.RelativeColumn(1); // Total
                                });

                                table.Header(h =>
                                {
                                    h.Cell().Element(c => HeaderCell(c, primary)).Text("Service");
                                    h.Cell().Element(c => HeaderCell(c, primary)).AlignRight().Text("Prix");
                                    h.Cell().Element(c => HeaderCell(c, primary)).AlignCenter().Text("Quantité");
                                    h.Cell().Element(c => HeaderCell(c, primary)).AlignRight().Text("Total");
                                });

                                var rowIndex = 0;
                                foreach (var line in engagement.Lines)
                                {
                                    var bg = rowIndex % 2 == 0 ? Colors.White : zebra;

                                    table.Cell().Element(c => DataCell(c, bg, border)).Text(line.ServiceNameSnapshot);
                                    table.Cell().Element(c => DataCell(c, bg, border)).AlignRight()
                                        .Text(FormatMoney(line.PriceSnapshot));
                                    table.Cell().Element(c => DataCell(c, bg, border)).AlignCenter()
                                        .Text($"{line.Quantity.Value} mois");
                                    table.Cell().Element(c => DataCell(c, bg, border)).AlignRight()
                                        .Text(FormatMoney(line.GetLineTotal()));

                                    rowIndex++;
                                }

                                // TOTAL (mise en évidence)
                                table.Cell().ColumnSpan(3).Element(c => TotalCell(c, soft, border))
                                    .Text("TOTAL").SemiBold();

                                table.Cell().Element(c => TotalCell(c, soft, border)).AlignRight()
                                    .Text(FormatMoney(engagement.TotalAmount)).SemiBold();
                            });
                        });
                    });

                    column.Item().Element(section =>
                    {
                        section.Column(col =>
                        {
                            col.Item().Element(c => { SectionTitle(c, "ÉCHÉANCIER DE PAIEMENT", primary); });

                            col.Item().PaddingTop(8).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(40); // N°
                                    columns.RelativeColumn(2);   // Date
                                    columns.RelativeColumn(2);   // Montant
                                    columns.RelativeColumn(1);   // Statut
                                });

                                table.Header(h =>
                                {
                                    h.Cell().Element(c => HeaderCell(c, primary)).AlignCenter().Text("N°");
                                    h.Cell().Element(c => HeaderCell(c, primary)).AlignCenter().Text("Date d'échéance");
                                    h.Cell().Element(c => HeaderCell(c, primary)).AlignRight().Text("Montant");
                                    h.Cell().Element(c => HeaderCell(c, primary)).AlignCenter().Text("Statut");
                                });

                                var trancheNumber = 1;
                                var rowIndex = 0;

                                foreach (var tranche in engagement.Tranches.OrderBy(t => t.DueDate))
                                {
                                    var bg = rowIndex % 2 == 0 ? Colors.White : zebra;

                                    table.Cell().Element(c => DataCell(c, bg, border)).AlignCenter()
                                        .Text(trancheNumber++.ToString());

                                    table.Cell().Element(c => DataCell(c, bg, border)).AlignCenter()
                                        .Text(tranche.DueDate.ToString("dd/MM/yyyy"));

                                    table.Cell().Element(c => DataCell(c, bg, border)).AlignRight()
                                        .Text(FormatMoney(tranche.AmountDue));

                                    table.Cell().Element(c => DataCell(c, bg, border)).AlignCenter().Text(t =>
                                    {
                                        // texte simple, mais tu peux aussi colorer selon statut
                                        t.Span(GetStatusText(tranche.Status)).SemiBold();
                                    });

                                    rowIndex++;
                                }
                            });
                        });
                    });
                });

                page.Footer().PaddingTop(10).Column(footer =>
                {
                    footer.Item().LineHorizontal(1).LineColor(border);
                    footer.Item().PaddingTop(6).Row(row =>
                    {

                        row.ConstantItem(50).AlignRight().Text(text =>
                        {
                            text.Span("Page ").FontSize(9).FontColor(Colors.Grey.Darken1);
                            text.CurrentPageNumber().FontSize(9).FontColor(Colors.Grey.Darken1);
                            text.Span(" / ").FontSize(9).FontColor(Colors.Grey.Darken1);
                            text.TotalPages().FontSize(9).FontColor(Colors.Grey.Darken1);
                        });
                    });
                });
            });
        }).GeneratePdf();
    }

    // ===== Styles helpers =====

    private static void SectionTitle(IContainer c, string title, string primary)
    {
        c.PaddingVertical(6).Row(row =>
        {
            row.ConstantItem(6).Height(18).Background(primary);
            row.RelativeItem().PaddingLeft(8)
                .Text(title).SemiBold().FontSize(12).FontColor(primary);
        });
    }

    private static IContainer HeaderCell(IContainer c, string primary)
    {
        return c.Background(primary)
            .PaddingVertical(6)
            .PaddingHorizontal(8)
            .DefaultTextStyle(x => x.SemiBold().FontColor(Colors.White).FontSize(10));
    }

    private static IContainer DataCell(IContainer c, string bg, string border)
    {
        return c.Background(bg)
            .BorderBottom(1).BorderColor(border)
            .PaddingVertical(6)
            .PaddingHorizontal(8);
    }

    private static IContainer TotalCell(IContainer c, string bg, string border)
    {
        return c.Background(bg)
            .BorderTop(1).BorderColor(border)
            .PaddingVertical(8)
            .PaddingHorizontal(8);
    }

    private static string FormatMoney(Money money)
        => $"{money.Amount:N2} {money.Currency}";

    private static string GetStatusText(TRANCHE_STATUS status)
    {
        return status switch
        {
            TRANCHE_STATUS.PENDING => "En attente",
            TRANCHE_STATUS.PAID => "Payé",
            TRANCHE_STATUS.OVERDUE => "En retard",
            _ => status.ToString()
        };
    }
}
