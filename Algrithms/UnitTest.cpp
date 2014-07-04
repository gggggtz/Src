/* 
 * File:   UnitTest.cpp
 * Author: root
 * 
 * Created on July 4, 2014, 9:50 AM
 */

#include "UnitTest.h"

CPPUNIT_TEST_SUITE_REGISTRATION(UnitTest);

UnitTest::UnitTest() {
}

UnitTest::~UnitTest() {
}

void UnitTest::setUp() {
    this->example = new int(1);
}

void UnitTest::tearDown() {
    delete this->example;
}

void UnitTest::testMethod() {
    CPPUNIT_ASSERT(*example == 1);
}

void UnitTest::testFailedMethod() {
    CPPUNIT_ASSERT(++*example == 1);
}
