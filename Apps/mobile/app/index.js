import React from 'react';
import Map from './components/Map';
import Login from './components/Login';
import Events from './components/Events/index';
import Ionicons from 'react-native-vector-icons/Ionicons';
import { NavigationContainer } from '@react-navigation/native';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';

const Tab = createBottomTabNavigator();

const App = () => {
  return (
    <NavigationContainer>
      <Tab.Navigator 
        initialRouteName='Map'
        screenOptions={({ route }) => ({
          headerShown: false,
          tabBarIcon: ({ focused, color, size }) => {
            let iconName;

            if (route.name === 'Login') {
              iconName = focused
                ? 'ios-information-circle'
                : 'ios-information-circle-outline';
            } else if (route.name === 'Map') {
              iconName = focused ? 'home' : 'home-outline';
            }
            return <Ionicons name={iconName} size={size} color={color} />;
          },
          tabBarActiveTintColor: '#00ff00',
          tabBarInactiveTintColor: '#ff0000',
        })}>
        <Tab.Screen name="Map" component={Map} />
        <Tab.Screen name="delivery" component={Deliveries} />
        <Tab.Screen name="Login" component={Login}/>
      </Tab.Navigator>
    </NavigationContainer>
  );
};

export default App;