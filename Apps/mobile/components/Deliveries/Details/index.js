import React from 'react';
import { View, Text, Image, StyleSheet } from 'react-native';
import { Button } from 'react-native-elements';
import evenement from '../../../Assets/Images/Evenements/paris.jpeg';

export default function DeliveryDetails() {
  return (
    <View>
      <View style={styles.surfaceSection}>
        <View style={styles.grid}>
          <View style={[styles.col12, styles.col6Lg]}>
            <View style={styles.flex}>
              <View style={[styles.pl3, styles.w10]}>
                <Image source={evenement} style={styles.image} />
              </View>
            </View>
          </View>
          <View style={[styles.col12, styles.col6Lg, styles.py5, styles.pl5]}>
            <View style={styles.list}>
              <Text style={styles.listItem}>Lieu</Text>
              <Text style={styles.listItem}>
                <Text style={styles.icon}>›</Text> Paris
              </Text>
              <Text style={styles.listItem}>
                <Text style={styles.icon}>›</Text> Tour Eiffel
              </Text>
            </View>
            <View style={styles.title}>
              <Text style={[styles.text, styles.bold]}>Tour Eiffel</Text>
              <Text style={[styles.text, styles.bold]}>Paris</Text>
            </View>
            <View style={[styles.flex, styles.alignCenter, styles.mb4]}>
              <Text style={styles.text}>
                <Text style={styles.bold}>20</Text> commentaires
              </Text>
            </View>
            <Text style={styles.description}>
              Dolor purus non enim praesent. At quis risus sed vulputate odio
              ut. Quis risus sed vulputate odio ut enim blandit volutpat. Ornare
              arcu odio ut sem nulla pharetra diam sit. Augue neque gravida in
              nrmentum et sollicitudin ac orci phasellus.
            </Text>
            <View style={[styles.flex, styles.alignCenter, styles.mb4]}>
              <Text style={[styles.text, styles.bold]}>Adresse</Text>
            </View>
            <Text style={styles.description}>
              Dolor purus non enim praesent. At quis risus sed vulputate odio
              ut. Quis risus sed vulputate odio ut enim blandit volutpat. Ornare
              arcu odio ut sem nulla pharetra diam sit. Augue neque gravida in
              nrmentum et sollicitudin ac orci phasellus.
            </Text>
            <Button
              icon={{ name: 'heart', type: 'font-awesome' }}
              buttonStyle={styles.button}
              title="Ajouter en favoris"
            />
          </View>
        </View>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  surfaceSection: {
    paddingVertical: 8,
    paddingHorizontal: 4,
    mdPaddingHorizontal: 6,
    lgPaddingHorizontal: 8,
  },
  grid: {
    marginBottom: 7,
    flexDirection: 'row',
  },
  col12: {
    width: '100%',
  },
  col6Lg: {
    width: '50%',
  },
  flex: {
    flex: 1,
  },
  pl3: {
    paddingLeft: 3,
  },
  w10: {
    width: '10%',
  },
  image: {
    width: '100%',
    },
});