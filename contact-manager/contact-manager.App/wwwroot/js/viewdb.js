let isDeleting = false;

function filterTable() {
    const filter = document.getElementById("filterInput").value.toLowerCase();
    const table = document.getElementById("peopleTable");
    const rows = table.getElementsByTagName("tr");

    for (let i = 1; i < rows.length; i++) {
        const cells = rows[i].getElementsByTagName("td");
        let found = false;
        for (let j = 0; j < cells.length - 1; j++) {
            if (cells[j].innerText.toLowerCase().indexOf(filter) > -1) {
                found = true;
                break;
            }
        }
        rows[i].style.display = found ? "" : "none";
    }
}

function sortTable(columnIndex) {
    const table = document.getElementById("peopleTable");
    const rows = Array.from(table.rows).slice(1);
    const isNumericColumn = (columnIndex === 1 || columnIndex === 4);
    const isMarriedColumn = (columnIndex === 2);

    rows.sort((a, b) => {
        const cellA = a.cells[columnIndex].innerText;
        const cellB = b.cells[columnIndex].innerText;

        if (isNumericColumn) {
            return parseFloat(cellA) - parseFloat(cellB);
        } else if (isMarriedColumn) {
            const checkboxA = a.cells[columnIndex].getElementsByTagName("input")[0];
            const checkboxB = b.cells[columnIndex].getElementsByTagName("input")[0];
            return (checkboxA.checked === checkboxB.checked) ? 0 : (checkboxA.checked ? -1 : 1);
        } else {
            return cellA.localeCompare(cellB);
        }
    });

    for (const row of rows) {
        table.appendChild(row);
    }
}

async function saveChanges(row) {
    if (isDeleting) return;

    const cells = row.getElementsByTagName("td");
    const id = row.dataset.id;
    const updatedData = {
        id: parseInt(id),
        name: cells[0].innerText,
        dateOfBirth: cells[1].innerText,
        married: cells[2].getElementsByTagName("input")[0].checked,
        phone: cells[3].innerText,
        salary: parseFloat(cells[4].innerText.replace(/,/g, ''))
    };

    try {
        const response = await fetch('/Home/Update', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedData)
        });

        if (response.ok) {
            alert('Changes saved successfully!');
        } else {
            const errorText = await response.text();
            console.error('Server response:', errorText);
            alert('Failed to save changes: ' + errorText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

function attachBlurEventHandlers() {
    const rows = document.querySelectorAll("#peopleTable tbody tr");
    rows.forEach(row => {
        row.addEventListener('blur', function () {
            saveChanges(this);
        }, true);
    });
}

document.addEventListener("DOMContentLoaded", attachBlurEventHandlers);

async function deleteRow(personId, event) {
    event.preventDefault();

    if (confirm('Are you sure you want to delete this record?')) {
        isDeleting = true;
        try {
            const response = await fetch(`/Home/Delete/${personId}`, {
                method: 'DELETE'
            });

            if (response.ok) {
                document.querySelector(`tr[data-id='${personId}']`).remove();
                alert('Record deleted successfully!');
            } else {
                alert('Failed to delete record.');
            }
        } catch (error) {
            console.error('Error:', error);
        } finally {
            isDeleting = false;
        }
    }
    event.stopPropagation();
}