
const uri = '/User';
let users = [];

let token = "";

function login() {
    const NameTextbox = document.getElementById('Name');
    const PasswordTextbox = document.getElementById('Password');

    const user = {
        name: NameTextbox.value.trim(),
        password: PasswordTextbox.value.trim(),
    };
    fetch('/User/Login', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        })
        .then(response => response.json())
        .then((response) => {
            token = response;
            window.sessionStorage.setItem('token',token);
        })
        .then(() => location.href = './html/Tasks.html')
        .catch(error => console.error('Unable to login', error));
}
