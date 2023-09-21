document.addEventListener("DOMContentLoaded", function () {
    let branchId = 1;
    initForm(branchId);
})

function initForm(branchId) {
    var url1 = "/api/RoomsApi/GetRoomTypes?branchId="+branchId;
    var url2 = "/api/RoomsApi/GetGuestNumbers"

    fetchJSON(url1).then(function (jsonData) {
        renderSelect(jsonData, "roomTypeId");
    })
    
    fetchJSON(url2).then(function (jsonData) {
        renderSelect(jsonData, "guestNumberId");
    })
};

function setDatapicker() {
    $("#datepicker1").datepicker();
    $("#datepicker2").datepicker();

    $("#datepicker1").click(function () {
        $("#datepicker1").addClass("form-select");
        $("#datepicker1").removeClass("form-control");
    });

    $("#datepicker2").click(function () {
        $("#datepicker2").addClass("form-select");
        $("#datepicker2").removeClass("form-control");
    });
}

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
