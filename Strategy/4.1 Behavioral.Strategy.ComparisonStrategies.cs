using System;
using System.Collections.Generic;
using System.Linq;

// todo: this is somehow buggy in recording
namespace DotNetDesignPatternDemos.Behavioral.Strategy
{
  class Person : IEquatable<Person>, IComparable<Person>
  {
    public int Id;
    public string Name;
    public int Age;

    public int CompareTo(Person other)
    {
      if (ReferenceEquals(this, other)) return 0;
      if (ReferenceEquals(null, other)) return 1;
      return Id.CompareTo(other.Id);
    }

    public Person(int id, string name, int age)
    {
      Id = id;
      Name = name;
      Age = age;
    }

    public bool Equals(Person other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Id == other.Id;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Person) obj);
    }

    public override int GetHashCode()
    {
      return Id;
    }

    public static bool operator ==(Person left, Person right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Person left, Person right)
    {
      return !Equals(left, right);
    }

    private sealed class NameRelationalComparer : IComparer<Person>
    {
      public int Compare(Person x, Person y)
      {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        return string.Compare(x.Name, y.Name, 
          StringComparison.Ordinal);
      }
    }

    public static IComparer<Person> NameComparer { get; } 
      = new NameRelationalComparer();
  }
  
  public class ComparisonStrategies
  {
    public static void Main(string[] args)
    {
      var people = new List<Person>();
      
      // equality == != and comparison < = >
      
      people.Sort(); // meaningless by default
      
      // sort by name with a lambda
      people.Sort((x, y) => x.Name.CompareTo(y.Name));
      
      people.Sort(Person.NameComparer);

    }
  }
}{"threads":[{"position":0,"start":0,"end":1137,"connection":"idle"},{"position":1138,"start":1138,"end":2273,"connection":"open"}],"url":"https://att-b.udemycdn.com/2019-06-14_20-37-34-3a441306ad94afd8e1e6689c32273b9b/original.txt?secure=cd3bKDemO3F9Nf6kzEp88Q%3D%3D%2C1615206455&filename=Behavioral.Strategy.ComparisonStrategies.cs.txt","method":"GET","port":443,"downloadSize":2273,"headers":{"date":"Mon, 08 Mar 2021 08:11:08 GMT","content-type":"text/plain","content-length":"2273","connection":"close","x-amz-id-2":"1clmOB4F+zachP81O2ysPba1k/WDpwkwW1FKZ9/XUoPi4uf7htLNOO6BLOaqdJSZ7mdVuoAFi8E=","x-amz-request-id":"6B218FC197D07C59","x-amz-replication-status":"COMPLETED","last-modified":"Fri, 14 Jun 2019 20:37:35 GMT","etag":"\"c302f65e14e4cf095ae61cd75b28530c\"","x-amz-meta-qqfilename":"Behavioral.Strategy.ComparisonStrategies.cs.txt","x-amz-version-id":"aG.XU.TEEV2XfYcfOGxHoSsu8wWSGTVt","x-edge-ip":"185.180.14.167","x-edge-location":"pragueCZ","x-cache":"HIT","x-age":"124516","server":"CDN77-Turbo","cache-control":"max-age=31536000","content-disposition":"attachment; filename=\"Behavioral.Strategy.ComparisonStrategies.cs.txt\"","x-77-nzt":"Abk73AHkeGz/rlm7AA==","x-77-nzt-ray":"zKhoYKRJF+0=","x-cache-lb":"HIT","x-age-lb":"12278190","x-77-cache":"HIT","x-77-pop":"frankfurtDE","accept-ranges":"bytes"}}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          