using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace WpfNotepad.Utility
{
    public static class UiExtensions
    {
        private static readonly Dictionary<PaperKind, PageMediaSizeName> _kindToSizeName;
        private static readonly Dictionary<PageMediaSizeName, PaperKind> _sizeNameToKind = new Dictionary<PageMediaSizeName, PaperKind>();

        static UiExtensions()
        {
            _kindToSizeName = new Dictionary<PaperKind, PageMediaSizeName>
            {
                {PaperKind.Custom,PageMediaSizeName.Unknown},
                {PaperKind.Letter, PageMediaSizeName.NorthAmericaLetter},
                {PaperKind.LetterSmall, PageMediaSizeName.NorthAmericaLetter},
                {PaperKind.Tabloid, PageMediaSizeName.NorthAmericaTabloid},
                {PaperKind.Ledger, PageMediaSizeName.NorthAmericaTabloid},
                {PaperKind.Legal, PageMediaSizeName.NorthAmericaLegal},
                {PaperKind.Statement, PageMediaSizeName.NorthAmericaStatement},
                {PaperKind.Executive, PageMediaSizeName.NorthAmericaExecutive},
                {PaperKind.A3, PageMediaSizeName.ISOA3},
                {PaperKind.A4, PageMediaSizeName.ISOA4},
                {PaperKind.A4Small, PageMediaSizeName.ISOA4},
                {PaperKind.A5, PageMediaSizeName.ISOA5},
                {PaperKind.B4, PageMediaSizeName.ISOB4},
                {PaperKind.B5, PageMediaSizeName.ISOB5Extra},
                {PaperKind.Folio, PageMediaSizeName.OtherMetricFolio},
                {PaperKind.Quarto, PageMediaSizeName.NorthAmericaQuarto},
                {PaperKind.Standard10x14, PageMediaSizeName.NorthAmerica10x14},
                {PaperKind.Standard11x17, PageMediaSizeName.NorthAmericaTabloid},
                {PaperKind.Note, PageMediaSizeName.NorthAmericaNote},
                {PaperKind.Number9Envelope, PageMediaSizeName.NorthAmericaNumber9Envelope},
                {PaperKind.Number10Envelope, PageMediaSizeName.NorthAmericaNumber10Envelope},
                {PaperKind.Number11Envelope, PageMediaSizeName.NorthAmericaNumber11Envelope},
                {PaperKind.Number12Envelope, PageMediaSizeName.NorthAmericaNumber12Envelope},
                {PaperKind.Number14Envelope, PageMediaSizeName.NorthAmericaNumber14Envelope},
                {PaperKind.CSheet, PageMediaSizeName.NorthAmericaCSheet},
                {PaperKind.DSheet, PageMediaSizeName.NorthAmericaDSheet},
                {PaperKind.ESheet, PageMediaSizeName.NorthAmericaESheet},
                {PaperKind.DLEnvelope, PageMediaSizeName.ISODLEnvelope},
                {PaperKind.C5Envelope, PageMediaSizeName.ISOC5Envelope},
                {PaperKind.C3Envelope, PageMediaSizeName.ISOC3Envelope},
                {PaperKind.C4Envelope, PageMediaSizeName.ISOC4Envelope},
                {PaperKind.C6Envelope, PageMediaSizeName.ISOC6Envelope},
                {PaperKind.C65Envelope, PageMediaSizeName.ISOC6C5Envelope},
                {PaperKind.B4Envelope, PageMediaSizeName.ISOB4Envelope},
                {PaperKind.B5Envelope, PageMediaSizeName.ISOB5Envelope},
                {PaperKind.B6Envelope, PageMediaSizeName.Unknown},
                {PaperKind.ItalyEnvelope, PageMediaSizeName.OtherMetricItalianEnvelope},
                {PaperKind.MonarchEnvelope, PageMediaSizeName.NorthAmericaMonarchEnvelope},
                {PaperKind.PersonalEnvelope, PageMediaSizeName.NorthAmericaPersonalEnvelope},
                {PaperKind.USStandardFanfold, PageMediaSizeName.Unknown},
                {PaperKind.GermanStandardFanfold, PageMediaSizeName.NorthAmericaGermanStandardFanfold},
                {PaperKind.GermanLegalFanfold, PageMediaSizeName.NorthAmericaGermanLegalFanfold},
                {PaperKind.IsoB4, PageMediaSizeName.ISOB4},
                {PaperKind.JapanesePostcard, PageMediaSizeName.JapanHagakiPostcard},
                {PaperKind.Standard9x11, PageMediaSizeName.Unknown},
                {PaperKind.Standard10x11, PageMediaSizeName.Unknown},
                {PaperKind.Standard15x11, PageMediaSizeName.Unknown},
                {PaperKind.InviteEnvelope, PageMediaSizeName.OtherMetricInviteEnvelope},
                {PaperKind.LetterExtra, PageMediaSizeName.NorthAmericaLetterExtra},
                {PaperKind.LegalExtra, PageMediaSizeName.NorthAmericaLegalExtra},
                {PaperKind.TabloidExtra, PageMediaSizeName.NorthAmericaTabloidExtra},
                {PaperKind.A4Extra, PageMediaSizeName.ISOA4Extra},
                {PaperKind.LetterTransverse, PageMediaSizeName.Unknown},
                {PaperKind.A4Transverse, PageMediaSizeName.Unknown},
                {PaperKind.LetterExtraTransverse, PageMediaSizeName.Unknown},
                {PaperKind.APlus, PageMediaSizeName.Unknown},
                {PaperKind.BPlus, PageMediaSizeName.Unknown},
                {PaperKind.LetterPlus, PageMediaSizeName.NorthAmericaLetterPlus},
                {PaperKind.A4Plus, PageMediaSizeName.OtherMetricA4Plus},
                {PaperKind.A5Transverse, PageMediaSizeName.Unknown},
                {PaperKind.B5Transverse, PageMediaSizeName.Unknown},
                {PaperKind.A3Extra, PageMediaSizeName.ISOA3Extra},
                {PaperKind.A5Extra, PageMediaSizeName.ISOA5Extra},
                {PaperKind.B5Extra, PageMediaSizeName.ISOB5Extra},
                {PaperKind.A2, PageMediaSizeName.ISOA2},
                {PaperKind.A3Transverse, PageMediaSizeName.Unknown},
                {PaperKind.A3ExtraTransverse, PageMediaSizeName.Unknown},
                {PaperKind.JapaneseDoublePostcard, PageMediaSizeName.JapanDoubleHagakiPostcard},
                {PaperKind.A6, PageMediaSizeName.ISOA6},
                {PaperKind.JapaneseEnvelopeKakuNumber2, PageMediaSizeName.JapanKaku2Envelope},
                {PaperKind.JapaneseEnvelopeKakuNumber3, PageMediaSizeName.JapanKaku3Envelope},
                {PaperKind.JapaneseEnvelopeChouNumber3, PageMediaSizeName.JapanChou3Envelope},
                {PaperKind.JapaneseEnvelopeChouNumber4, PageMediaSizeName.JapanChou4Envelope},
                {PaperKind.LetterRotated, PageMediaSizeName.NorthAmericaLetterRotated},
                {PaperKind.A3Rotated, PageMediaSizeName.ISOA3Rotated},
                {PaperKind.A4Rotated, PageMediaSizeName.ISOA4Rotated},
                {PaperKind.A5Rotated, PageMediaSizeName.ISOA5Rotated},
                {PaperKind.B4JisRotated, PageMediaSizeName.JISB4Rotated},
                {PaperKind.B5JisRotated, PageMediaSizeName.JISB5Rotated},
                {PaperKind.JapanesePostcardRotated, PageMediaSizeName.JapanHagakiPostcardRotated},
                {PaperKind.JapaneseDoublePostcardRotated, PageMediaSizeName.JapanHagakiPostcardRotated},
                {PaperKind.A6Rotated, PageMediaSizeName.ISOA6Rotated},
                {PaperKind.JapaneseEnvelopeKakuNumber2Rotated, PageMediaSizeName.JapanKaku2EnvelopeRotated},
                {PaperKind.JapaneseEnvelopeKakuNumber3Rotated, PageMediaSizeName.JapanKaku3EnvelopeRotated},
                {PaperKind.JapaneseEnvelopeChouNumber3Rotated, PageMediaSizeName.JapanChou3EnvelopeRotated},
                {PaperKind.JapaneseEnvelopeChouNumber4Rotated, PageMediaSizeName.JapanChou4EnvelopeRotated},
                {PaperKind.B6Jis, PageMediaSizeName.JISB6},
                {PaperKind.B6JisRotated, PageMediaSizeName.JISB6Rotated},
                {PaperKind.Standard12x11, PageMediaSizeName.Unknown},
                {PaperKind.JapaneseEnvelopeYouNumber4, PageMediaSizeName.JapanYou4Envelope},
                {PaperKind.JapaneseEnvelopeYouNumber4Rotated, PageMediaSizeName.JapanYou4EnvelopeRotated},
                {PaperKind.Prc16K, PageMediaSizeName.PRC16K},
                {PaperKind.Prc32K, PageMediaSizeName.PRC32K},
                {PaperKind.Prc32KBig, PageMediaSizeName.PRC32KBig},
                {PaperKind.PrcEnvelopeNumber1, PageMediaSizeName.PRC1Envelope},
                {PaperKind.PrcEnvelopeNumber2, PageMediaSizeName.PRC2Envelope},
                {PaperKind.PrcEnvelopeNumber3, PageMediaSizeName.PRC3Envelope},
                {PaperKind.PrcEnvelopeNumber4, PageMediaSizeName.PRC4Envelope},
                {PaperKind.PrcEnvelopeNumber5, PageMediaSizeName.PRC5Envelope},
                {PaperKind.PrcEnvelopeNumber6, PageMediaSizeName.PRC6Envelope},
                {PaperKind.PrcEnvelopeNumber7, PageMediaSizeName.PRC7Envelope},
                {PaperKind.PrcEnvelopeNumber8, PageMediaSizeName.PRC8Envelope},
                {PaperKind.PrcEnvelopeNumber9, PageMediaSizeName.PRC9Envelope},
                {PaperKind.PrcEnvelopeNumber10, PageMediaSizeName.PRC10Envelope},
                {PaperKind.Prc16KRotated, PageMediaSizeName.PRC16KRotated},
                {PaperKind.Prc32KRotated, PageMediaSizeName.PRC32KRotated},
                {PaperKind.Prc32KBigRotated, PageMediaSizeName.Unknown},
                {PaperKind.PrcEnvelopeNumber1Rotated, PageMediaSizeName.PRC1EnvelopeRotated},
                {PaperKind.PrcEnvelopeNumber2Rotated, PageMediaSizeName.PRC2EnvelopeRotated},
                {PaperKind.PrcEnvelopeNumber3Rotated, PageMediaSizeName.PRC3EnvelopeRotated},
                {PaperKind.PrcEnvelopeNumber4Rotated, PageMediaSizeName.PRC4EnvelopeRotated},
                {PaperKind.PrcEnvelopeNumber5Rotated, PageMediaSizeName.PRC5EnvelopeRotated},
                {PaperKind.PrcEnvelopeNumber6Rotated, PageMediaSizeName.PRC6EnvelopeRotated},
                {PaperKind.PrcEnvelopeNumber7Rotated, PageMediaSizeName.PRC7EnvelopeRotated},
                {PaperKind.PrcEnvelopeNumber8Rotated, PageMediaSizeName.PRC8EnvelopeRotated},
                {PaperKind.PrcEnvelopeNumber9Rotated, PageMediaSizeName.PRC9EnvelopeRotated},
                {PaperKind.PrcEnvelopeNumber10Rotated, PageMediaSizeName.PRC10EnvelopeRotated},

            };

            foreach (var item in _kindToSizeName)
            {
                _sizeNameToKind[item.Value] = item.Key;
            }
            _sizeNameToKind[PageMediaSizeName.Unknown] = PaperKind.Custom;
        }

        public static PageMediaSizeName ToPageMediaSizeName(this PaperKind paperKind)
        {
            PageMediaSizeName pageMediaSizeName;
            return _kindToSizeName.TryGetValue(paperKind, out pageMediaSizeName) == true ? pageMediaSizeName : PageMediaSizeName.Unknown;
        }

        public static PaperKind ToPaperKind(this PageMediaSizeName pageMediaSizeName)
        {
            PaperKind paperKind;
            return _sizeNameToKind.TryGetValue(pageMediaSizeName, out paperKind) == true ? paperKind : PaperKind.Custom;
        }
    }
}
