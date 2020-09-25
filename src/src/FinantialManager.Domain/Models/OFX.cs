using System.IO;
using System.Text;
using System.Xml.Serialization;
using NetDevPack.Domain;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using FinantialManager.Infra.CrossCutting.Util.Extensions;

namespace FinantialManager.Domain.Models
{
    public class OFX : IAggregateRoot
    {
        [Key]
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string FileOFX { get; set; }

        public string GenerateOFXId()
        {
            string input = $"{this.SIGNONMSGSRSV1.SONRS.DTSERVER.ToUpper().Replace(" ", string.Empty)}" +
                           $"{this.BANKMSGSRSV1.STMTTRNRS.TRNUID.ToUpper().Replace(" ", string.Empty)}" +
                           $"{this.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKACCTFROM.BANKID}" +
                           $"{this.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKACCTFROM.ACCTID.ToUpper().Replace(" ", string.Empty)}" +
                           $"{this.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKTRANLIST.DTSTART.ToUpper().Replace(" ", string.Empty)}" +
                           $"{this.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKTRANLIST.DTEND.ToUpper().Replace(" ", string.Empty)}";

            return input.GetHash();
        }

        public string GenerateAccountId()
        {
            string inputAcc = $"{this.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKACCTFROM.BANKID}" +
                              $"{this.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKACCTFROM.ACCTID.ToUpper().Replace(" ", string.Empty)}";

            return inputAcc.GetHash();
        }

        private SIGNONMSGSRSV1 sIGNONMSGSRSV1Field;

        private BANKMSGSRSV1 bANKMSGSRSV1Field;

        /// <remarks/>
        public SIGNONMSGSRSV1 SIGNONMSGSRSV1
        {
            get
            {
                return this.sIGNONMSGSRSV1Field;
            }
            set
            {
                this.sIGNONMSGSRSV1Field = value;
            }
        }

        /// <remarks/>
        public BANKMSGSRSV1 BANKMSGSRSV1
        {
            get
            {
                return this.bANKMSGSRSV1Field;
            }
            set
            {
                this.bANKMSGSRSV1Field = value;
            }
        }

        public static OFX GetOFXFromString(string input)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(OFX));
            MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            return (OFX)serializer.Deserialize(memStream);
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SIGNONMSGSRSV1
    {
        [Key]
        private string _Id;
        public string Id
        {
            get
            {
                if (_Id == null || _Id == string.Empty)
                {
                    var input = $"{SONRS.DTSERVER}{SONRS.LANGUAGE}{SONRS.STATUS.CODE}";
                    _Id = input.GetHash();
                }

                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        private SONRS sONRSField;

        /// <remarks/>
        public SONRS SONRS
        {
            get
            {
                return this.sONRSField;
            }
            set
            {
                this.sONRSField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SONRS
    {
        [Key]
        private string _Id;
        public string Id
        {
            get
            {
                if (_Id == null || _Id == string.Empty)
                {
                    var input = $"{DTSERVER}{LANGUAGE}{STATUS.CODE}";
                    _Id = input.GetHash();
                }

                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        private STATUS sTATUSField;

        private string dTSERVERField;

        private string lANGUAGEField;

        /// <remarks/>
        public STATUS STATUS
        {
            get
            {
                return this.sTATUSField;
            }
            set
            {
                this.sTATUSField = value;
            }
        }

        /// <remarks/>
        public string DTSERVER
        {
            get
            {
                return this.dTSERVERField;
            }
            set
            {
                this.dTSERVERField = value;
            }
        }

        /// <remarks/>
        public string LANGUAGE
        {
            get
            {
                return this.lANGUAGEField;
            }
            set
            {
                this.lANGUAGEField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class STATUS
    {

        private byte cODEField;

        private string sEVERITYField;

        /// <remarks/>
        [Key]
        public byte CODE
        {
            get
            {
                return this.cODEField;
            }
            set
            {
                this.cODEField = value;
            }
        }

        /// <remarks/>
        public string SEVERITY
        {
            get
            {
                return this.sEVERITYField;
            }
            set
            {
                this.sEVERITYField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class BANKMSGSRSV1
    {
        [Key]
        private string _Id;
        public string Id
        {
            get
            {
                if (_Id == null || _Id == string.Empty)
                {
                    var input = $"{STMTTRNRS.TRNUID}{STMTTRNRS.STMTRS.CURDEF}{STMTTRNRS.STMTRS.LEDGERBAL}{STMTTRNRS.STMTRS.BANKACCTFROM.BANKID}{STMTTRNRS.STMTRS.BANKACCTFROM.ACCTID}";
                    _Id = input.GetHash();
                }

                return _Id;
            }
            set
            {
                _Id = value;
            }
        }
        private STMTTRNRS sTMTTRNRSField;

        /// <remarks/>
        public STMTTRNRS STMTTRNRS
        {
            get
            {
                return this.sTMTTRNRSField;
            }
            set
            {
                this.sTMTTRNRSField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class STMTTRNRS
    {
        [Key]
        private string _Id;
        public string Id
        {
            get
            {
                if (_Id == null || _Id == string.Empty)
                {
                    var input = $"{TRNUID}{STMTRS.CURDEF}{STMTRS.LEDGERBAL}{STMTRS.BANKACCTFROM.BANKID}{STMTRS.BANKACCTFROM.ACCTID}";
                    _Id = input.GetHash();
                }

                return _Id;
            }
            set
            {
                _Id = value;
            }
        }
        private string tRNUIDField;

        private STATUS sTATUSField;

        private STMTRS sTMTRSField;

        /// <remarks/>
        public string TRNUID
        {
            get
            {
                return this.tRNUIDField;
            }
            set
            {
                this.tRNUIDField = value;
            }
        }

        /// <remarks/>
        public STATUS STATUS
        {
            get
            {
                return this.sTATUSField;
            }
            set
            {
                this.sTATUSField = value;
            }
        }

        /// <remarks/>
        public STMTRS STMTRS
        {
            get
            {
                return this.sTMTRSField;
            }
            set
            {
                this.sTMTRSField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class STMTRS
    {
        [Key]
        private string _Id;
        public string Id
        {
            get
            {
                if (_Id == null || _Id == string.Empty)
                {
                    var input = $"{CURDEF}{LEDGERBAL}{BANKACCTFROM.BANKID}{BANKACCTFROM.ACCTID}";
                    _Id = input.GetHash();
                }

                return _Id;
            }
            set
            {
                _Id = value;
            }
        }
        private string cURDEFField;

        private BANKACCTFROM bANKACCTFROMField;

        private BANKTRANLIST bANKTRANLISTField;

        private LEDGERBAL lEDGERBALField;

        /// <remarks/>
        public string CURDEF
        {
            get
            {
                return this.cURDEFField;
            }
            set
            {
                this.cURDEFField = value;
            }
        }

        /// <remarks/>
        public BANKACCTFROM BANKACCTFROM
        {
            get
            {
                return this.bANKACCTFROMField;
            }
            set
            {
                this.bANKACCTFROMField = value;
            }
        }

        /// <remarks/>
        public BANKTRANLIST BANKTRANLIST
        {
            get
            {
                return this.bANKTRANLISTField;
            }
            set
            {
                this.bANKTRANLISTField = value;
            }
        }

        /// <remarks/>
        public LEDGERBAL LEDGERBAL
        {
            get
            {
                return this.lEDGERBALField;
            }
            set
            {
                this.lEDGERBALField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class BANKACCTFROM
    {
        [Key]
        public string Id { get; set; }
        private ushort bANKIDField;

        private string aCCTIDField;

        private string aCCTTYPEField;

        /// <remarks/>
        public ushort BANKID
        {
            get
            {
                return this.bANKIDField;
            }
            set
            {
                this.bANKIDField = value;
            }
        }

        /// <remarks/>
        public string ACCTID
        {
            get
            {
                return this.aCCTIDField;
            }
            set
            {
                this.aCCTIDField = value;
            }
        }

        /// <remarks/>
        public string ACCTTYPE
        {
            get
            {
                return this.aCCTTYPEField;
            }
            set
            {
                this.aCCTTYPEField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class BANKTRANLIST
    {
        [Key]
        private string _Id;
        public string Id
        {
            get
            {
                if (_Id == null || _Id == string.Empty)
                {
                    var input = $"{DTSTART}{DTEND}";
                    _Id = input.GetHash();
                }

                return _Id;
            }
            set
            {
                _Id = value;
            }
        }
        private string dTSTARTField;

        private string dTENDField;

        private List<STMTTRN> sTMTTRNField;

        /// <remarks/>
        public string DTSTART
        {
            get
            {
                return this.dTSTARTField;
            }
            set
            {
                this.dTSTARTField = value;
            }
        }

        /// <remarks/>
        public string DTEND
        {
            get
            {
                return this.dTENDField;
            }
            set
            {
                this.dTENDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("STMTTRN")]
        public List<STMTTRN> STMTTRN
        {
            get
            {
                return this.sTMTTRNField;
            }
            set
            {
                this.sTMTTRNField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class STMTTRN
    {
        [Key]
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string OFXId { get; set; }

        private string tRNTYPEField;

        private string dTPOSTEDField;

        private decimal tRNAMTField;

        private string mEMOField;

        /// <remarks/>
        public string TRNTYPE
        {
            get
            {
                return this.tRNTYPEField;
            }
            set
            {
                this.tRNTYPEField = value;
            }
        }

        /// <remarks/>
        public string DTPOSTED
        {
            get
            {
                return this.dTPOSTEDField;
            }
            set
            {
                this.dTPOSTEDField = value;
            }
        }

        /// <remarks/>
        public decimal TRNAMT
        {
            get
            {
                return this.tRNAMTField;
            }
            set
            {
                this.tRNAMTField = value;
            }
        }

        /// <remarks/>
        public string MEMO
        {
            get
            {
                return this.mEMOField;
            }
            set
            {
                this.mEMOField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LEDGERBAL
    {
        [Key]
        private string _Id;
        public string Id
        {
            get
            {
                if (_Id == null || _Id == string.Empty)
                {
                    var input = $"{BALAMT}{DTASOF}";
                    _Id = input.GetHash();
                }

                return _Id;
            }
            set
            {
                _Id = value;
            }
        }
        private decimal bALAMTField;

        private string dTASOFField;

        /// <remarks/>
        public decimal BALAMT
        {
            get
            {
                return this.bALAMTField;
            }
            set
            {
                this.bALAMTField = value;
            }
        }

        /// <remarks/>
        public string DTASOF
        {
            get
            {
                return this.dTASOFField;
            }
            set
            {
                this.dTASOFField = value;
            }
        }
    }
}