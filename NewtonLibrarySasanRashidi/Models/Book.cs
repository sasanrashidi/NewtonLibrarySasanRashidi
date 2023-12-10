using System;
using System.ComponentModel.DataAnnotations;

namespace NewtonLibrarySasanRashidi.Models
{
    public class Book
    {
        //Egenskaper för en bok...
        public int Id { get; set; } // Bokens ID-nummer

        [MaxLength(50)]
        public string? Title { get; set; } // Bokens titel med maxlängd 50 tecken
        public int? Year { get; set; } // Bokens publiceringsår
        public Guid Isbn { get; set; } = Guid.NewGuid(); // Bokens ISBN med ett unikt standardvärde
        public int Grade { get; set; } = new Random().Next(1, 10); // Bokens betyg mellan 1 och 9

        //Låneinfo för boken
        private int? _userCardId;
        private DateTime? _loanDate;

        public bool BookIsLoaned // Anger om boken är utlånad eller inte
        {
            get => _userCardId.HasValue; // Returnerar true om bokens lånkorts-ID har ett värde
            set // Sätter lånestatusen för boken
            {
                if (value && !_loanDate.HasValue) StartLoan(); // Startar boklånet om sann och ingen lånedatum finns
                else if (!value) EndLoan(); // Avslutar boklånet om falsk
            }
        }

        public DateTime? LoanDate // Bokens lånedatum
        {
            get => _loanDate;
            set // Sätter bokens lånedatum
            {
                _loanDate = value;

                if (BookIsLoaned && _loanDate == null) StartLoan(); // Startar boklånet om utlånad och inget datum
                else if (!BookIsLoaned) EndLoan(); // Avslutar boklånet om inte utlånad
            }
        }

        public DateTime? ReturnDate { get; private set; } // Bokens återlämningsdatum

        public int? UserCardId // Bokens lånkorts-ID
        {
            get => _userCardId;
            set
            {
                _userCardId = value;

                if (value == null) EndLoan(); // Avslutar boklånet om värdet är null
                else if (BookIsLoaned) StartLoan(); // Startar boklånet om utlånad
            }

        }

        private void StartLoan() // Startar ett boklån
        {
            _loanDate = DateTime.Now; // Sätter lånedatumet till nuvarande datum
            ReturnDate = _loanDate?.AddDays(14);  // Beräknar återlämningsdatum om 14 dagar
        }

        private void EndLoan() // Avslutar ett boklån
        {
            _loanDate = null; // Återställer lånedatumet
            ReturnDate = null; // Återställer återlämningsdatumet
        }

        public UserCard? UserCard { get; set; }  // Lånekortsinformation för boken
        public ICollection<Author>? Authors { get; set; } // Författare för boken


    }
}

