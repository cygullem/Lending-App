function calculateAmounts() {
    const amount = parseFloat(document.getElementById('amount').value) || 0;
    const interest = parseFloat(document.getElementById('interest').value) || 0;
    const deduct = parseFloat(document.getElementById('deduct').value) || 0;

    const interestAmount = (amount * interest) / 100;
    const recievable = amount - deduct;

    document.getElementById('interestedAmount').value = interestAmount.toFixed(2);
    document.getElementById('recievable').value = recievable.toFixed(2);
}

document.getElementById('amount').addEventListener('input', calculateAmounts);
document.getElementById('interest').addEventListener('input', calculateAmounts);
document.getElementById('deduct').addEventListener('input', calculateAmounts);


// async function ViewTransaction(itemId) {
//     try {
//         const response = await fetch(`/Loan/ViewTransaction?id=${itemId}`);
//         if (response.ok) {
//             const data = await response.text();
//             document.getElementById('modalList').innerHTML = data;
//         } else {
//             console.error('Failed to fetch data');
//         }
//     } catch (error) {
//         console.error('Error:', error);
//     }
// }

async function ViewTransaction(itemId) {
    try {
        const response = await fetch(`/Loan/ViewTransaction?id=${itemId}`);
        if (response.ok) {
            const data = await response.text();
            document.getElementById('modalList').innerHTML = data;
            openModal();
        } else {
            console.error('Failed to fetch data');
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

function openModal() {
    document.getElementById('transactionModal').classList.remove('hidden');
    document.getElementById('transactionModal').classList.add('modal-open');
}

function closeModal() {
    document.getElementById('transactionModal').classList.add('hidden');
    document.getElementById('transactionModal').classList.remove('modal-open');
}