using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class debug : MonoBehaviour {
	// Use this for initialization
	void Start () {
        //UnityEngine.Debug.Log(typeof(BussinessSlot).IsAssignableFrom(typeof(BussinessSlot)));
        //UnityEngine.Debug.Log(typeof(GearSlot).IsAssignableFrom(typeof(BussinessSlot)));
        //UnityEngine.Debug.Log(typeof(BaseSlot).IsAssignableFrom(typeof(BussinessSlot)));
        //A a = new B();
        //A aa = new D();
        //Debug.Log(a is B);
        //Debug.Log(a is C);
        //Debug.Log(aa is A);
        //Debug.Log(aa is B);
        //LinkedList<int> vs = new LinkedList<int>();
        //vs.AddFirst(0);
        //vs.AddLast(1);
        //vs.AddLast(2);
        //vs.AddLast(3);
        //vs.AddLast(4);
        ////vs.AddFirst(5);
        //LinkedListNode<int> linkedListNode = new LinkedListNode<int>(2);
        //vs.AddLast(linkedListNode);
        //foreach (int i in vs)
        //{
        //    Debug.Log(i);
        //}
        //A a = new A();
        //A aa = new A();
        //Debug.Log(a.Equals(aa));
        //Debug.Log(vs.Find(3).Value);
        //vs.Remove(3);
        //foreach (int i in vs)
        //{
        //    Debug.Log("removeAfter:" + i);
        //}
        //Debug.Log(vs.Last.Next+ "??？"+ vs.First.Previous );
        //A a = new B();
        //A aa = new C();
        //TestClass testClass = new TestClass();
        //testClass.Test(a);
        //testClass.Test(aa);
        //List<A> @as = new List<A>();
        //@as.Add(new A(1));
        //@as.Add(new A(2));
        //@as.Add(new A(3));
        //@as.Add(new A(4));
        //@as.Add(new A(5));
        ////Debug.Log(@as.Count);
        //@as.RemoveAt(0);
        //@as.RemoveAt(0);
        //@as.RemoveAt(1);
        //Debug.Log(@as[0].ddd +"```" + @as[1].ddd );
        //A.GetA.testcEventHandle += new A.testc(test);
        //A.GetA.TriggerTest();
        //renderer = this.gameObject.GetComponent<CanvasRenderer>();
        //Debug.Log(renderer.inView);
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        Debug.Log(keyValuePairs.TryGet("sss"));
    }

    // Update is called once per frame
    void Update () {
       
	}

    private void test()
    {
        Debug.Log("1111");
    }
}

public  class A:Ac
{
    private Dictionary<int, string> dict = new Dictionary<int, string>();
    public A()
    {
        
    }
    public delegate void testc();
    public event testc testcEventHandle;
    public void TriggerTest() { testcEventHandle(); }
}

public class B : A
{
    public B()
    {

    }
}

public class C : A
{

}

//public class D : B
//{

//}

public class TestClass
{
    public void Test(Ac ac)
    {
        //GetClass getClass = new GetClass();
        //getClass.GetClassTest(ac as A);
    }
}

public class GetClass
{
    //public void GetClassTest(A a)
    //{
    //    if (a is B)
    //    {
    //        Debug.Log("这是B");
    //    }
    //    if (a is C)
    //    {
    //        Debug.Log("这是C");
    //    }
    //    if (a is A)
    //    {
    //        Debug.Log("A");
    //    }
    //}
}

public interface Ac { }
public interface Td { }