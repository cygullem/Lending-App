function calculateAge(birthday) {
    const today = new Date();
    const dob = new Date(birthday);
    let age = today.getFullYear() - dob.getFullYear();
    const monthDiff = today.getMonth() - dob.getMonth();

    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < dob.getDate())) {
        age--;
    }

    return age;
}
document.getElementById('birthday').addEventListener('change', function() {
    const birthday = this.value;
    const ageField = document.getElementById('age');

    if (birthday) {
        const age = calculateAge(birthday);
        ageField.value = age;
    } else {
        ageField.value = '';
    }
}); 


