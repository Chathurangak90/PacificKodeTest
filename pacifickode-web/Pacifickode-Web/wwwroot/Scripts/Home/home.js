$(document).ready(function () {
    // Call load on page ready
    loadDepEmpCount();
});

// Function to load department and employee counts
function loadDepEmpCount() {
    $.ajax({
        url: window.base_url + 'api/Home/depempcount',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#department-count').text(data.departmentCount);
            $('#employee-count').text(data.employeeCount);
        },
        error: function (xhr, status, error) {
            console.error('Error fetching counts:', error);
            $('#department-count').text('N/A');
            $('#employee-count').text('N/A');
        }
    });
}