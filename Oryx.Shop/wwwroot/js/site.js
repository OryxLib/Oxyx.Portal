function InitResult(result, success,fail) {
    if (result.Success) {
        SuccessMessage('操作成功');
        if (success) {
            success(result.Date)
        }
    } else {
        ErrorMessage(result.Message);
        console.error(result);
        if (fail) {
            fail();
        }
    }
}