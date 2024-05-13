const uri = 'api/Products';
let products = [];

function getProducts() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayProducts(data))
        .catch(error => console.error('Unable to get products.', error));
}

function addProduct() {
    const addNameTextbox = document.getElementById('add-name');
    const addPriceTextbox = document.getElementById('add-price');
    const addCategoryIdTextbox = document.getElementById('add-categoryId');
    const addIsAvailableCheckbox = document.getElementById('add-isAvailable');

    const product = {
        name: addNameTextbox.value.trim(),
        price: parseFloat(addPriceTextbox.value.trim()),
        categoryId: parseInt(addCategoryIdTextbox.value.trim()),
        isAvailable: addIsAvailableCheckbox.checked
    };

    document.getElementById('error').style.display = 'none';
    document.getElementById('error').textContent = ''; 

    fetch('api/Products', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(product)
    })
        .then(response => {
        if (!response.ok) {
            return response.json().then(err => { throw err; });
        }
        return response.json();
    })
        .then(data => {
            getProducts();
            addNameTextbox.value = '';
            addPriceTextbox.value = '';
            addCategoryIdTextbox.value = '';
            addIsAvailableCheckbox.checked = false;
        }).catch((error) => {
            console.error('Error:', error);
            document.getElementById('error').style.display = 'inline-block';
            if (typeof error === 'object') {
                
                let message = '';
                for (let key in error) {
                    message += key + ': ' + error[key] + '\n';
                }
                document.getElementById('error').textContent = message;
            } else {
                
                document.getElementById('error').textContent = error;
            }
        });
}

function deleteProduct(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getProducts())
        .catch(error => console.error('Unable to delete product.', error));
}

function displayEditForm(id) {
    const product = products.find(product => product.id === id);

    document.getElementById('edit-id').value = product.id;
    document.getElementById('edit-name').value = product.name;
    document.getElementById('edit-price').value = product.price;
    document.getElementById('edit-categoryId').value = product.categoryId;
    document.getElementById('edit-isAvailable').checked = product.isAvailable;
    document.getElementById('editForm').style.display = 'block';
}

function updateProduct() {
    const productId = document.getElementById('edit-id').value;
    const product = {
        id: parseInt(productId, 10),
        name: document.getElementById('edit-name').value.trim(),
        price: parseFloat(document.getElementById('edit-price').value.trim()),
        categoryId: parseInt(document.getElementById('edit-categoryId').value.trim()),
        isAvailable: document.getElementById('edit-isAvailable').checked
    };

    fetch(`${uri}/${productId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(product)
    })
        .then(() => getProducts())
        .catch(error => console.error('Unable to update product.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayProducts(data) {
    const tBody = document.getElementById('products');
    tBody.innerHTML = '';

    const button = document.createElement('button');

    data.forEach(product => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Редагувати';
        editButton.setAttribute('onclick', `displayEditForm(${product.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Видалити';
        deleteButton.setAttribute('onclick', `deleteProduct(${product.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(product.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNodePrice = document.createTextNode(product.price);
        td2.appendChild(textNodePrice);

        let td3 = tr.insertCell(2);
        let textNodeCategoryId = document.createTextNode(product.categoryId);
        td3.appendChild(textNodeCategoryId);

        let td4 = tr.insertCell(3);
        let textNodeIsAvailable = document.createTextNode(product.isAvailable ? 'Так' : 'Ні');
        td4.appendChild(textNodeIsAvailable);

        let td5 = tr.insertCell(4);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(5);
        td6.appendChild(deleteButton);
    });

    products = data;
}



