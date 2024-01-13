function handleRadioChange(radio, checkboxes) {
    radio.forEach((item) => {
        item.addEventListener("change", (e) => {
            checkboxes.forEach((checkbox) => {
                if (e.target.id === "select") {
                    checkbox.checked = false;
                    checkbox.disabled = false;
                    checkbox.onclick = () => true;
                } else if (e.target.id === "full") {
                    checkbox.checked = true;
                    checkbox.disabled = false;
                    checkbox.onclick = () => false;
                } else {
                    checkbox.checked = false;
                    checkbox.disabled = true;
                }
            });
        });
    });
}

const radio = document.querySelectorAll(".role");
const checkboxes = document.querySelectorAll(".checkbox_role");
handleRadioChange(radio, checkboxes);

const radio1 = document.querySelectorAll(".location");
const checkboxes1 = document.querySelectorAll(".checkbox_location");
handleRadioChange(radio1, checkboxes1);
