<template>
  <span class="inline-block">
    <span v-for="option in options" :key="option[idField]"
      @click="$emit('input', option[idField])" class="pointer inline-block">
      <span class="inline-block align-middle check circle bg-white shadow0">
        <div class="block w100 h100 circle bg-blue transition"
          v-if="option[idField] == value" />
      </span>
      <span class="mx1 bold upcase">
        {{ option[labelField] }}
      </span>
    </span>
  </span>
</template>

<script>
  export default {
    name: "RadioGroup",
    props: {
      value: {
        type: [ Number, String ],
        default: null,
      },
      options: {
        type: Array,
        default: () => [],
      },
      idField: {
        type: String,
        default: "id",
      },
      labelField: {
        type: String,
        default: "name",
      },
      container: {
        type: String,
        default: "span",
      },
    },
  };
</script>

<style lang="stylus" scoped>
  .check
    width 2em
    height 2em
    padding 4px
</style>
