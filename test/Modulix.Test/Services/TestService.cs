namespace Modulix.Test.Fixture;

public class ChildService(GlobalService _)
{
}

public class ExposedService(ChildService _)
{
}

public class HiddenService(ChildService _)
{
}

public class GlobalService
{
}
