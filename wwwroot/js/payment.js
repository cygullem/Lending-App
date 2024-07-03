// function getPayment(LoanId, total, PaymentId) {
//     document.getElementById("LoanId").value = LoanId;
//     document.getElementById("PaymentId").value = PaymentId;
//     document.getElementById("currentAmmount").value = total;
//     document.getElementById("totalPayments").innerHTML = total;
//   }
  
//   const totalPaymentsElement = document.getElementById("totalPayments");
//   const remainingBalanceElement = document.getElementById("remainingPayments");
//   const amountInput = document.getElementById("amount");
  
//   function updateRemainingBalance() {
//     const amountPaid = parseFloat(totalPaymentsElement.textContent);
//     const amountToPay = parseFloat(amountInput.value) || 0;
//     const remainingBalance = amountPaid - amountToPay;
//     remainingBalanceElement.textContent = remainingBalance.toFixed(2);
//   }
  
//   amountInput.addEventListener("input", updateRemainingBalance);


function openModal(LoanId, total, PaymentId) {
  document.getElementById("LoanId").value = LoanId;
  document.getElementById("PaymentId").value = PaymentId;
  document.getElementById("currentAmmount").value = total;
  document.getElementById("totalPayments").innerHTML = total;
  document.getElementById('payLoanModal').classList.add('modal-open');
}

function closeModal() {
  document.getElementById('payLoanModal').classList.remove('modal-open');
}

const totalPaymentsElement = document.getElementById("totalPayments");
const remainingBalanceElement = document.getElementById("remainingPayments");
const amountInput = document.getElementById("amount");

function updateRemainingBalance() {
  const amountPaid = parseFloat(totalPaymentsElement.textContent);
  const amountToPay = parseFloat(amountInput.value) || 0;
  const remainingBalance = amountPaid - amountToPay;
  remainingBalanceElement.textContent = remainingBalance.toFixed(2);
}

amountInput.addEventListener("input", updateRemainingBalance);