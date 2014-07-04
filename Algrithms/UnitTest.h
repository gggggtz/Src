/* 
 * File:   UnitTest.h
 * Author: root
 *
 * Created on July 4, 2014, 9:50 AM
 */

#ifndef UNITTEST_H
#define	UNITTEST_H

#include <cppunit/extensions/HelperMacros.h>

class UnitTest : public CPPUNIT_NS::TestFixture {
    CPPUNIT_TEST_SUITE(UnitTest);
    CPPUNIT_TEST(testMethod);
    CPPUNIT_TEST(testFailedMethod);
    CPPUNIT_TEST_SUITE_END();

public:
    UnitTest();
    virtual ~UnitTest();
    void setUp();
    void tearDown();

private:
    int *example;
    void testMethod();
    void testFailedMethod();
};

#endif	/* UNITTEST_H */

