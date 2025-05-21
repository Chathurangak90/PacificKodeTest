$(document).ready(function () {

    // Initialize jQuery Validate on the form
    $("#departments").validate({
        ignore: ":hidden:not(select)",
        errorClass: "my-error-class form-control-danger",
        validClass: "my-valid-class",

        // Validation rules for form inputs
        rules: {
            'department-code': {
                required: true
            },
            'department-name': {
                required: true
            }
        },
        // Custom error messages
        messages: {
            'department-code': "Department Code is required",
            'department-name': "Department Name is required"
        },

        // Form submission handler if validation passes
        submitHandler: function (form) {
            let checkbtncheck = $("#btn-save").text(); // Check if we are updating or saving new data

            if (checkbtncheck == 'Update') {
                updateData(); // Call update function
            } else {
                saveData(); // Call save function
            }
        }
    });

    // Edit button click event handler
    $(document).on('click', '.edit-btn', function () {
        const row = $(this).closest('tr'); // Get parent table row
        const id = row.find('td:eq(1)').text(); // Get hidden ID
        const code = row.find('td:eq(2)').text(); // Get department code
        const name = row.find('td:eq(3)').text(); // Get department name

        // Populate form fields with existing data
        $('#department-id').val(id);
        $('#department-code').val(code);
        $('#department-name').val(name);
        $("#btn-save").text('Update'); // Change button text to 'Update'
    });

    // Delete button click event handler
    $(document).on('click', '.delete-btn', function (e) {
        e.preventDefault();
        const departmentId = $(this).data('id'); // Get department ID from data attribute
        const confirmed = confirm("Are you sure you want to delete this department?");

        if (confirmed) {
            // Send DELETE request to API
            $.ajax({
                url: window.base_url + 'api/Department/deletedepartment/' + departmentId,
                type: 'DELETE',
                success: function (res) {
                    alert(res.message); // Show API message
                    getAllDepartments(); // Reload the table
                    clearData(); // Clear the form
                },
                error: function (xhr, status, error) {
                    console.error(error);
                    alert("Failed to delete department.");
                }
            });
        }
    });

    // 'New' button click event handler to reset the form
    $('#btn-new').on('click', function () {
        clearData(); // Clear form fields and reset validation
    });

    // Initial load of department list
    getAllDepartments();
});

// Function to fetch and display all departments in the table
function getAllDepartments() {
    $.ajax({
        url: window.base_url + 'api/Department/loadalldepartment',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var tbody = $("#departmentBody");
            tbody.empty(); // Clear existing table rows

            // Loop through department data and build table rows
            $.each(data, function (index, item) {
                var row = `
                    <tr>
                        <td>${index + 1}</td>
                        <td class="d-none">${item.departmentId}</td>
                        <td>${item.departmentCode}</td>
                        <td>${item.departmentName}</td>
                        <td>
                            <button class="btn btn-sm btn-outline-primary me-1 edit-btn">Edit</button>
                            <button class="btn btn-sm btn-outline-danger delete-btn" data-id="${item.departmentId}">Delete</button>
                        </td>
                    </tr>`;
                tbody.append(row); // Append row to table body
            });
        },
        error: function (xhr, status, error) {
            console.error(error); // Log error in console
        }
    });
}

// Function to update an existing department
function updateData() {
    var department = {
        DepartmentId: $('#department-id').val(),
        DepartmentCode: $('#department-code').val(),
        DepartmentName: $('#department-name').val()
    };

    $.ajax({
        url: window.base_url + 'api/Department/updatedepartment',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(department),
        success: function (res) {
            alert(res.message); // Show response message
            getAllDepartments(); // Reload table
            clearData(); // Clear form
        },
        error: function (xhr, status, error) {
            console.error(error);
            alert('Error updating department.');
        }
    });
}

// Function to save a new department
function saveData() {
    var department = {
        DepartmentId: $('#department-id').val(),
        DepartmentCode: $('#department-code').val(),
        DepartmentName: $('#department-name').val()
    };

    $.ajax({
        url: window.base_url + 'api/Department/savedepartment',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(department),
        success: function (res) {
            alert(res.message); // Show response message
            getAllDepartments(); // Reload table
            clearData(); // Clear form
        },
        error: function (xhr, status, error) {
            console.error(error);
            alert('Error saving department.');
        }
    });
}

// Function to clear/reset form fields and validation
function clearData() {
    $('#department-id').val(0);
    $('#department-code').val("");
    $('#department-name').val("");
    $("#btn-save").text('Save');

    // Reset validation error messages and states
    var validator = $("#departments").validate();
    validator.resetForm();
}
