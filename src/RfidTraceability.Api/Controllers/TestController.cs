using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase {
    
    private readonly MiServicio _m1;
    private readonly MiServicio _m2;

    public TestController(MiServicio m1, MiServicio m2)
    {
        _m1 = m1;
        _m2 = m2;
    }

    [HttpGet("transient")]
    public ActionResult<object> GetTransient()
    {
        return Ok(new {id1 = _m1.GetId(), id2 = _m2.GetId()});
    }
}