using CSharpExamples;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpExamplesTests
{
    public class FileEnumeratorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HappyPath()
        {
            var fileEnumerator = new FileEnumerator(@".\TestFiles\FileEnumeratorTestFile.txt", ',');
            List<List<string>> dataSets = new List<List<string>>();
            int dataSetCount = 0;

            foreach (var dataSet in fileEnumerator)
            {
                dataSets.Add(dataSet.GetRange(0, dataSet.Count));
                dataSetCount++;
            }

            Assert.AreEqual(3, dataSetCount);

            var firstDataSet = dataSets[0];
            Assert.NotNull(firstDataSet);
            Assert.AreEqual(5, firstDataSet.Count);

            var secondDataSet = dataSets[1];
            Assert.NotNull(secondDataSet);
            Assert.AreEqual(2, secondDataSet.Count);

            var thirdDataSet = dataSets[2];
            Assert.NotNull(thirdDataSet);
            Assert.AreEqual(2, thirdDataSet.Count);
        }

        [Test]
        public void FileNotFoundException()
        {
            var fileEnumerator = new FileEnumerator(@".\TestFiles\NonExistentFile.txt", ',');
            Exception thrownException = null;

            try
            {
                foreach (var dataSet in fileEnumerator)
                {

                }
            }
            catch (Exception exception)
            {
                thrownException = exception;
            }
            
            Assert.NotNull(thrownException);
            Assert.IsInstanceOf<FileNotFoundException>(thrownException);
        }
    }
}