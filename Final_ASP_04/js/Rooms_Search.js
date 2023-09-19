document.addEventListener("DOMContentLoaded", function () {
    initForm();
})

function initForm() {
    var url1 = "/api/RoomsApi/GetRoomTypes"
    var url2 = "/api/RoomsApi/GetGuestNumbers"

    fetchJSON(url1).then(function (jsonData) {
        renderSelect(jsonData, "roomTypeId");
    })

    fetchJSON(url2).then(function (jsonData) {
        renderSelect(jsonData, "guestNumberId");
    })
};

function fetchJSON(url) {
    return fetch(url, {
        method: "GET",
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(function (response) {
        return response.json();
    })
};

function renderSelect(jsonData, elementId) {
    let targetSelect = document.getElementById(elementId);
    let dataSource = jsonData.unshift({ id: 0, name: "請選擇" });

    let options = jsonData.map(function (item) {
        return `<option value="${item.id}">${item.name}</option>`;
    });

    targetSelect.innerHTML = options.join("");
};
